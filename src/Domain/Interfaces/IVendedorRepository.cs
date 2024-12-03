using VendasLitoral.Application.DTOs;
using VendasLitoral.Domain.Models;

namespace VendasLitoral.Infrastructure.Interfaces;

public interface IVendedorRepository
{
    Task<List<Vendedor>> GetAllVendedores();
    Task<Vendedor?> GetVendedorById(int id);
    Task<Vendedor?> UpdateVendedor(int id, VendedorDTO vendedorDto);
    Task<Vendedor?> CreateVendedor(VendedorDTO vendedorDto);
    Task<bool> DeleteVendedor(int id);
    Task<List<VendasDto?>> VendasNoPeriodo(string dataInicial, string dataFinal);
    Task<object?> MelhorCliente();
}