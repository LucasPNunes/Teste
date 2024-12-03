using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendasLitoral.Domain.Models;

[Table("PEDIDO")]
public class Pedido
{   
    [Key]
    public int Id { get; set; }
    public string DescricaoPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public string Observacao { get; set; }
    public bool Autorizado { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public int VendedorId { get; set; }
    public Vendedor Vendedor { get; set; }
}