using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendasLitoral.Domain.Models;

[Table("VENDEDOR")]
public class Vendedor
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string CodigoVendedor { get; set; }
    public string Apelido { get; set; }
    public bool Ativo { get; set; }
}