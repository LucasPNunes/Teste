using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VendasLitoral.Application.DTOs;
using VendasLitoral.Domain.Models;
using VendasLitoral.Infrastructure.Interfaces;

namespace VendasLitoral.Application.Controllers;

[Route("Pedidos")]
[ApiController]
public class PedidoController : Controller
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoController(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetPedidos()
    {
        var pedidos = await _pedidoRepository.GetAllPedidos();
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPedidoById([FromRoute] int id)
    {
        var pedido = await _pedidoRepository.GetPedidoById(id);
        return Ok(pedido);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePedido([FromBody] PedidoDTO pedidoData)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var pedido = await _pedidoRepository.CreatePedido(pedidoData);
        if (pedido != null)
            return Ok(pedido);
        return BadRequest("Erro ao criar pedido.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePedido([FromBody] PedidoDTO pedidoData, [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var pedido = await _pedidoRepository.UpdatePedido(pedidoData, id);
        if (pedido != null)
            return Ok(pedido);
        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePedido([FromRoute] int id)
    {
        var success = await _pedidoRepository.DeletePedido(id);
        if(success)
            return NoContent();
        return NotFound();
    }
}