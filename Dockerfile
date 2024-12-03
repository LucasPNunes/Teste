FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
EXPOSE 8080
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["VendasLitoral.csproj", "./"]
RUN dotnet restore "VendasLitoral.csproj"

COPY . .  

RUN dotnet clean "VendasLitoral.csproj"
RUN dotnet build "VendasLitoral.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VendasLitoral.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=publish /app/publish . 

RUN dotnet ef database update --no-build

ENTRYPOINT ["dotnet", "VendasLitoral.dll"]
