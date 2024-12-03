using Microsoft.AspNetCore.Mvc;
using VendasLitoral.Application.DTOs;
using VendasLitoral.Domain.Models;
using VendasLitoral.Infrastructure.Interfaces;

namespace VendasLitoral.Application.Controllers;


[Route("Clientes")]
[ApiController]
public class ClienteController : Controller
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteController(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetClientes()
    {
        var clientes = await _clienteRepository.GetAllClientes();
        return Ok(clientes);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientesById([FromRoute] int id)
    {
        var cliente = await _clienteRepository.GetClienteById(id);
        if (cliente == null)
            return NotFound();
        return Ok(cliente);
    }

    [HttpGet("ComprasNoPeriodo")]
    public async Task<IActionResult> ComprasNoPeriodo([FromQuery] string dataInicial, [FromQuery] string dataFinal, [FromQuery] int id)
    {
        try
        {
            var valor = _clienteRepository.ComprasNoPeriodo(dataInicial, dataFinal, id);
            return Ok(valor);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCliente([FromBody] ClienteDTO clienteData)
    {   
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var clienteCriado = await _clienteRepository.CreateCliente(clienteData);
        if (clienteCriado)
            return Created();
        return BadRequest("Erro ao criar cliente.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCliente([FromRoute] int id, [FromBody] ClienteDTO clienteData)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var cliente = await _clienteRepository.UpdateCliente(id, clienteData);
        return Ok(cliente);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente([FromRoute] int id)
    {
        var success = await _clienteRepository.DeleteCliente(id);
        if(success)
            return NoContent();
        return StatusCode(500, "Erro ao deletar cliente.");
    }
}