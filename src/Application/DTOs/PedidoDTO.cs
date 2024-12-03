using VendasLitoral.Domain.Models;

namespace VendasLitoral.Application.DTOs;

public class PedidoDTO
{
    public string DescricaoPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public string Observacao { get; set; }
    public bool Autorizado { get; set; }
    public int ClienteId { get; set; }
    public int VendedorId { get; set; }
}