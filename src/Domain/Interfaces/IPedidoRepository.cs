using VendasLitoral.Application.DTOs;
using VendasLitoral.Domain.Models;

namespace VendasLitoral.Infrastructure.Interfaces;

public interface IPedidoRepository
{
    Task<List<Pedido>> GetAllPedidos();
    Task<Pedido?> GetPedidoById(int id);
    Task<Pedido?> CreatePedido(PedidoDTO pedidoData);
    Task<bool> DeletePedido(int id);
    Task<Pedido?> UpdatePedido(PedidoDTO pedidoData, int id);
}