using Microsoft.EntityFrameworkCore;
using VendasLitoral.Application.DTOs;
using VendasLitoral.Application.Mappers;
using VendasLitoral.Domain.Models;
using VendasLitoral.Infrastructure.Context;
using VendasLitoral.Infrastructure.Interfaces;

namespace VendasLitoral.Domain.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ConnectionContext _context;

    public ClienteRepository(ConnectionContext context)
    {
        _context = context;
    }
    public async Task<List<Cliente>> GetAllClientes()
    {
        var clientes = await _context.CLIENTE.ToListAsync();
        return clientes;
    }

    public async Task<Cliente?> GetClienteById(int id)
    {
        var cliente = await _context.CLIENTE.Where(c => c.Id == id).FirstOrDefaultAsync();
        if(cliente != null)
            return cliente;
        return null;
    }

    public async Task<bool> CreateCliente(ClienteDTO clienteData)
    {
        var newCliente = clienteData.ToCliente();
        await _context.CLIENTE.AddAsync(newCliente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Cliente?> UpdateCliente(int id, ClienteDTO clienteData)
    {
        var clienteExistente = await GetClienteById(id);
        if (clienteExistente == null) return null;
        clienteExistente = clienteData.ToCliente(id);
        await _context.SaveChangesAsync();
        return clienteExistente;
    }

    public async Task<bool> DeleteCliente(int id)
    {
        var clienteExistente = await GetClienteById(id);
        if (clienteExistente == null) return false;
        _context.CLIENTE.Remove(clienteExistente);
        await _context.SaveChangesAsync();
        return true;
    }

    public decimal ComprasNoPeriodo(string dataInicial, string dataFinal, int id)
    {
        try
        {
            DateTime? inicio = ConverteData(dataInicial);
            DateTime? fim = ConverteData(dataFinal);
            if (inicio != null && fim != null)
            {
                var compras = _context.PEDIDO
                    .Where(p => p.ClienteId == id && p.DataCriacao >= inicio && p.DataCriacao <= fim)
                    .Select(p => p.ValorTotal)
                    .ToList();
                if (compras.Any())
                    return compras.Sum();
                return 0m;
            }

            throw new Exception("Erro no formato das datas.");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public DateTime? ConverteData(string dataStr)
    {
        DateTime dataFormat;
        bool sucesso = DateTime.TryParseExact(dataStr, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out dataFormat);
        if (sucesso) 
            return dataFormat;
        return null;
    }
}