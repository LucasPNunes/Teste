using VendasLitoral.Application.DTOs;
using VendasLitoral.Domain.Models;

namespace VendasLitoral.Application.Mappers;

public static class VendedorMapper
{
    public static Vendedor ToVendedor(this VendedorDTO v)
    {
        return new Vendedor
        {
            Nome = v.Nome,
            CodigoVendedor = v.CodigoVendedor,
            Apelido = v.Apelido,
            Ativo = v.Ativo
        };
    }
    public static Vendedor ToVendedor(this VendedorDTO v, int id)
    {
        return new Vendedor
        {
            Id = id,
            Nome = v.Nome,
            CodigoVendedor = v.CodigoVendedor,
            Apelido = v.Apelido,
            Ativo = v.Ativo
        };
    }
    public static VendedorDTO ToVendedorDTO(this Vendedor v)
    {
        return new VendedorDTO
        {
            Nome = v.Nome,
            CodigoVendedor = v.CodigoVendedor,
            Apelido = v.Apelido,
            Ativo = v.Ativo
        };
    }
}