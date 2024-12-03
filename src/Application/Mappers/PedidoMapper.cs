using VendasLitoral.Application.DTOs;
using VendasLitoral.Domain.Models;

namespace VendasLitoral.Application.Mappers;

public static class PedidoMapper
{
    public static Pedido ToPedido(this PedidoDTO p)
    {
        return new Pedido
        {
            DescricaoPedido = p.DescricaoPedido,
            ValorTotal = p.ValorTotal,
            Observacao = p.Observacao,
            Autorizado = p.Autorizado,
            ClienteId = p.ClienteId,
            VendedorId = p.VendedorId
        };
    }
    public static Pedido ToPedido(this PedidoDTO p, int id)
    {
        return new Pedido
        {
            Id = id,
            DescricaoPedido = p.DescricaoPedido,
            ValorTotal = p.ValorTotal,
            Observacao = p.Observacao,
            Autorizado = p.Autorizado,
            ClienteId = p.ClienteId,
            VendedorId = p.VendedorId
        };
    }
}