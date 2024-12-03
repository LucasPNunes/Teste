using VendasLitoral.Application.DTOs;
using VendasLitoral.Domain.Models;

namespace VendasLitoral.Infrastructure.Interfaces;

public interface IClienteRepository
{
    Task<List<Cliente>> GetAllClientes();
    Task<Cliente?> GetClienteById(int id);
    Task<bool> CreateCliente(ClienteDTO clienteData);
    Task<Cliente?> UpdateCliente(int id, ClienteDTO clienteData);
    Task<bool> DeleteCliente(int id);
    decimal ComprasNoPeriodo(string dataInicial, string dataFinal, int id);
}