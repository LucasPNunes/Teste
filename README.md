---

# **README: Como Rodar o Projeto**

Este projeto consiste em duas partes principais: o **Backend** (ASP.NET Core) e o **Frontend** (Angular). Abaixo estão as instruções para rodar ambos os sistemas localmente, sem o uso do Docker.

---

## **Pré-requisitos**

Antes de rodar o projeto, certifique-se de ter as seguintes dependências instaladas na sua máquina:

### **1. Dependências Comuns**

- **.NET SDK 8.0 ou superior**: Para rodar o backend.
  - [Download do .NET SDK](https://dotnet.microsoft.com/download/dotnet)
  
- **Node.js e npm**: Para rodar o frontend.
  - [Download do Node.js](https://nodejs.org/)
  
- **Angular CLI**: Para rodar o projeto Angular.
  - Para instalar globalmente, execute o seguinte comando no terminal:
    ```bash
    npm install -g @angular/cli
    ```

### **2. Dependências Backend (ASP.NET Core)**

- **SQL Server**.
  - [Download do SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
  
- **Banco de Dados SQL**: O projeto depende de uma conexão com SQL Server, conforme configurado no arquivo `appsettings.json`.

### **3. Dependências Frontend (Angular)**

- **Angular CLI**: Já foi mencionado anteriormente para rodar o projeto Angular.

---

## **Rodando o Backend (ASP.NET Core)**

### **1. Clonando o Repositório**

Primeiro, clone o repositório para sua máquina:

```bash
git clone <url-do-repositorio>
cd <diretorio-do-repositorio>
```

### **2. Configuração do Banco de Dados**

- **Instale o SQL Server** (ou use uma instância existente).
- Você pode executar uma query utilizando o arquivo `create_database.sql`. 
- Também é necessario um usuario chamado root cadastrado com a senha `123@` que tenha acesso a base Litoral. 

A string de conexão está no arquivo `appsettings.json` e se parece com isso:
É necessario alterar a string de conexão para rodar na sua maquina.

```json
"ConnectionStrings": {
  "DBConnection": "Server=<Sua conexão SQL Server>;Database=Litoral;User Id=root;Password=123@;TrustServerCertificate=true;"
}
```

### **3. Restaurando Dependências**

No terminal, dentro da pasta do projeto (onde está o arquivo `.csproj`), execute o seguinte comando para restaurar as dependências do backend:

```bash
dotnet restore
```

### **4. Build e Execução do Backend**

Após restaurar as dependências, compile e execute o backend com o comando:

```bash
dotnet build
dotnet run
```

O backend estará rodando em `http://localhost:5018`.

### **5. Teste da API**

- Uma documentação simplificada sobre as rotas disponiveis estará visivel na rota: `http://localhost:5000/swagger` (caso esteja em desenvolvimento).
- Você pode testar as rotas da API diretamente no Swagger.

---

## **Rodando o Frontend (Angular)**

### **1. Clonando o Repositório**


```bash
cd src/FrontEnd
```

### **2. Instalando as Dependências**

Dentro da pasta do frontend, execute o seguinte comando para instalar as dependências do Angular:

```bash
npm install
```

### **3. Executando o Frontend**

Após instalar as dependências, inicie o servidor de desenvolvimento do Angular:

```bash
ng serve --open
```

O Angular estará rodando por padrão em `http://localhost:4200`.

  
### **4. Teste do Frontend**

Após o Angular ser iniciado, você deve ser capaz de navegar pelas rotas, como `http://localhost:4200/dashboard`, `http://localhost:4200/pedidos`, como foi especificado na documentação do teste.

---

