
namespace VendasLitoral.Application.DTOs;

public class ClienteDTO
{
    public string RazaoSocial { get; set; }
    public string NomeFantasia { get; set; }
    public string CNPJ { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public bool Ativo { get; set; }
}