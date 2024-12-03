using VendasLitoral.Application.DTOs;
using VendasLitoral.Domain.Models;

namespace VendasLitoral.Application.Mappers;

public static class ClienteMapper
{
    public static ClienteDTO ToClienteDTO(this Cliente c)
    {
        return new ClienteDTO
        {
            RazaoSocial = c.RazaoSocial,
            NomeFantasia = c.NomeFantasia,
            CNPJ = c.CNPJ,
            Logradouro = c.Logradouro,
            Bairro = c.Bairro,
            Cidade = c.Cidade,
            Estado = c.Estado,
            Ativo = c.Ativo,
        };
    }

    public static Cliente ToCliente(this ClienteDTO c, int id)
    {
        return new Cliente
        {
            Id = id,
            RazaoSocial = c.RazaoSocial,
            NomeFantasia = c.NomeFantasia,
            CNPJ = c.CNPJ,
            Logradouro = c.Logradouro,
            Bairro = c.Bairro,
            Cidade = c.Cidade,
            Estado = c.Estado,
            Ativo = c.Ativo,
        };
    }

    public static Cliente ToCliente(this ClienteDTO c)
    {
        return new Cliente
        {
            RazaoSocial = c.RazaoSocial,
            NomeFantasia = c.NomeFantasia,
            CNPJ = c.CNPJ,
            Logradouro = c.Logradouro,
            Bairro = c.Bairro,
            Cidade = c.Cidade,
            Estado = c.Estado,
            Ativo = c.Ativo,
        };
    }
}