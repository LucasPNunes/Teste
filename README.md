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
git clone https://github.com/LucasPNunes/Teste.git
```

### **2. Configuração do Banco de Dados**

- **Instale o SQL Server** (ou use uma instância existente).
- Você pode executar o arquivo SQL para criar o banco de dados juntamente com a view e a procedure: `create_database.sql`. 
```SQL
  CREATE DATABASE Litoral
GO
USE Litoral
GO
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [CLIENTE] (
    [Id] int NOT NULL IDENTITY,
    [RazaoSocial] nvarchar(max) NOT NULL,
    [NomeFantasia] nvarchar(max) NOT NULL,
    [CNPJ] nvarchar(450) NOT NULL,
    [Logradouro] nvarchar(max) NOT NULL,
    [Bairro] nvarchar(max) NOT NULL,
    [Cidade] nvarchar(max) NOT NULL,
    [Estado] nvarchar(max) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_CLIENTE] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [VENDEDOR] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [CodigoVendedor] nvarchar(450) NOT NULL,
    [Apelido] nvarchar(max) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_VENDEDOR] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PEDIDO] (
    [Id] int NOT NULL IDENTITY,
    [DescricaoPedido] nvarchar(max) NOT NULL,
    [ValorTotal] decimal(18,2) NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    [Observacao] nvarchar(max) NOT NULL,
    [Autorizado] bit NOT NULL,
    [ClienteId] int NOT NULL,
    [VendedorId] int NOT NULL,
    CONSTRAINT [PK_PEDIDO] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PEDIDO_CLIENTE_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [CLIENTE] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PEDIDO_VENDEDOR_VendedorId] FOREIGN KEY ([VendedorId]) REFERENCES [VENDEDOR] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_CLIENTE_CNPJ] ON [CLIENTE] ([CNPJ]);
GO

CREATE INDEX [IX_PEDIDO_ClienteId] ON [PEDIDO] ([ClienteId]);
GO

CREATE INDEX [IX_PEDIDO_VendedorId] ON [PEDIDO] ([VendedorId]);
GO

CREATE UNIQUE INDEX [IX_VENDEDOR_CodigoVendedor] ON [VENDEDOR] ([CodigoVendedor]);
GO

CREATE PROCEDURE [dbo].[VendasPorVendedor]
    @VendedorId INT
AS
BEGIN
    SELECT
        p.Id AS PedidoId,
        p.DescricaoPedido,
        p.ValorTotal,
        p.DataCriacao,
        p.Observacao,
        p.Autorizado,
        c.NomeFantasia,
        c.CNPJ,
        v.Nome AS VendedorNome
    FROM PEDIDO p
    JOIN CLIENTE c ON p.ClienteId = c.Id
    JOIN VENDEDOR v ON p.VendedorId = v.Id
    WHERE p.VendedorId = @VendedorId
    ORDER BY p.DataCriacao;
END;
GO

CREATE VIEW vw_PedidosClientesVendedores
AS
SELECT
    p.DescricaoPedido,
    p.ValorTotal,
    p.DataCriacao,
    p.Observacao,
    p.Autorizado,
    c.NomeFantasia,
    c.CNPJ,
    v.Nome AS VendedorNome
FROM PEDIDO p
JOIN CLIENTE c ON p.ClienteId = c.Id
JOIN VENDEDOR v ON p.VendedorId = v.Id;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241201020138_init', N'8.0.0');
GO

COMMIT;
GO

```
 
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

