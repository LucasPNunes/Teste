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
