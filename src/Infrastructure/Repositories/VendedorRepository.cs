using Microsoft.EntityFrameworkCore;
using VendasLitoral.Application.DTOs;
using VendasLitoral.Application.Mappers;
using VendasLitoral.Domain.Models;
using VendasLitoral.Infrastructure.Context;
using VendasLitoral.Infrastructure.Interfaces;

namespace VendasLitoral.Domain.Repositories;

public class VendedorRepository : IVendedorRepository
{
    private readonly ConnectionContext _context;

    public VendedorRepository(ConnectionContext context)
    {
        _context = context;
    }
    public async Task<List<Vendedor>> GetAllVendedores()
    {
        var vendedores = await _context.VENDEDOR.ToListAsync();
        return vendedores;
    }

    public async Task<Vendedor?> GetVendedorById(int id)
    {
        var vendedor = await _context.VENDEDOR.FirstOrDefaultAsync(v => v.Id == id);
        if (vendedor != null) 
            return vendedor;
        return null;
    }

    public async Task<Vendedor?> UpdateVendedor(int id, VendedorDTO vendedorDto)
    {
        var vendedorExistente = await GetVendedorById(id);
        if (vendedorExistente == null) 
            return null;
        vendedorExistente = vendedorDto.ToVendedor(id);
        await _context.SaveChangesAsync();
        return (vendedorExistente);
    }

    public async Task<Vendedor?> CreateVendedor(VendedorDTO vendedorDto)
    {
        var newVendedor = vendedorDto.ToVendedor();
        await _context.VENDEDOR.AddAsync(newVendedor);
        await _context.SaveChangesAsync();
        return newVendedor;
    }

    public async Task<bool> DeleteVendedor(int id)
    {
        var vendedorExistente = await GetVendedorById(id);
        if (vendedorExistente == null) return false;
        _context.Remove(vendedorExistente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<VendasDto?>> VendasNoPeriodo(string dataInicial, string dataFinal)
    {
        try
        {
            DateTime? inicio = ConverteData(dataInicial);
            DateTime? fim = ConverteData(dataFinal);
        
            if (inicio == null || fim == null)
            {
                throw new ArgumentException("Erro no formato das datas.");
            }
        
            var vendas = await _context.PEDIDO
                .Where(p => p.DataCriacao >= inicio && p.DataCriacao <= fim)
                .Include(p => p.Vendedor)
                .GroupBy(p => p.VendedorId)
                .Select(p => new VendasDto
                {
                    Nome = p.FirstOrDefault().Vendedor.Nome,
                    Apelido = p.FirstOrDefault().Vendedor.Apelido,
                    Codigo = p.FirstOrDefault().Vendedor.CodigoVendedor,
                    Total = p.Sum(x => x.ValorTotal)
                })
                .ToListAsync();

            return vendas;
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao recuperar vendas no período: {e.Message}", e);
        }
    }



    public async Task<object?> MelhorCliente()
    {
        var melhorCliente = await _context.PEDIDO
            .Include(p => p.Cliente)
            .GroupBy(p => p.ClienteId)
            .Select(g => new
            {
                Cliente = g.FirstOrDefault().Cliente,
                totalCompras = g.Sum(p => p.ValorTotal)
            })
            .OrderByDescending(x => x.totalCompras)
            .FirstOrDefaultAsync();
        return melhorCliente;
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