using Microsoft.EntityFrameworkCore;
using VendasLitoral.Application.DTOs;
using VendasLitoral.Application.Mappers;
using VendasLitoral.Domain.Models;
using VendasLitoral.Infrastructure.Context;
using VendasLitoral.Infrastructure.Interfaces;

namespace VendasLitoral.Domain.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly ConnectionContext _context;
    
    public PedidoRepository(ConnectionContext context)
    {
        _context = context;
    }
    
    public async Task<List<Pedido>> GetAllPedidos()
    {
        var pedidos = await _context.PEDIDO.OrderBy(p => p.DataCriacao)
            .Include(p => p.Cliente)
            .Include(p => p.Vendedor)
            .ToListAsync();
        return pedidos;
    }
    
    public async Task<Pedido?> GetPedidoById(int id)
    {
        var pedido = await _context.PEDIDO
            .Include(p => p.Cliente)
            .Include(p => p.Vendedor)
            .FirstOrDefaultAsync(p => p.Id == id);
        return pedido;
    }
    
    public async Task<Pedido?> CreatePedido(PedidoDTO pedidoData)
    {
        var newPedido = pedidoData.ToPedido();
        await _context.PEDIDO.AddAsync(newPedido);
        await _context.SaveChangesAsync();
        return newPedido;
    }
    
    public async Task<Pedido?> UpdatePedido(PedidoDTO pedidoData, int id)
    {
        var pedidoExistente = await GetPedidoById(id);
        if (pedidoExistente == null) return null;
        pedidoExistente = pedidoData.ToPedido(id);
        await _context.SaveChangesAsync();
        return pedidoExistente;
    }
    
    public async Task<bool> DeletePedido(int id)
    {
        var pedidoExistente = await GetPedidoById(id);
        if (pedidoExistente == null) return false;
        _context.PEDIDO.Remove(pedidoExistente);
        await _context.SaveChangesAsync();
        return true;
    }
}