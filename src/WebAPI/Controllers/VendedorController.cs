using Microsoft.AspNetCore.Mvc;
using VendasLitoral.Application.DTOs;
using VendasLitoral.Domain.Models;
using VendasLitoral.Infrastructure.Interfaces;

namespace VendasLitoral.Application.Controllers;


[Route("Vendedores")]
[ApiController]
public class VendedorController : Controller
{
    private readonly IVendedorRepository _vendedorRepository;
    
    public VendedorController(IVendedorRepository vendedorRepository)
    {
        _vendedorRepository = vendedorRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetVendedores()
    {
        var vendedores = await _vendedorRepository.GetAllVendedores();
        return Ok(vendedores);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetVendedorById(int id)
    {
        var vendedor = await _vendedorRepository.GetVendedorById(id);

        if (vendedor == null)
            return NotFound();

        return Ok(vendedor);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateVendedor([FromBody] VendedorDTO vendedorDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var vendedorCriado = await _vendedorRepository.CreateVendedor(vendedorDTO);
        if (vendedorCriado != null)
            return Ok(vendedorCriado);
        return BadRequest("Erro ao criar cliente.");
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVendedor(int id, [FromBody] VendedorDTO vendedorDTO)
    {
        var vendedorAtualizado = await _vendedorRepository.UpdateVendedor(id, vendedorDTO);
        if (vendedorAtualizado == null)
            return NotFound();
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVendedor(int id)
    {
        var vendedorDesativado = await _vendedorRepository.DeleteVendedor(id);

        if (!vendedorDesativado)
            return NotFound();

        return NoContent();
    }
    
    [HttpGet("VendasNoPeriodo")]
    public async Task<ActionResult> GetVendasNoPeriodo([FromQuery] string dataInicial, [FromQuery] string dataFinal)
    {
        var vendasTotal = await _vendedorRepository.VendasNoPeriodo(dataInicial, dataFinal);
        return Ok(vendasTotal);
    }
    
    [HttpGet("MelhorCliente")]
    public async Task<ActionResult> GetMelhorCliente()
    {
        var cliente = await _vendedorRepository.MelhorCliente();
        if (cliente == null)
            return NotFound();
        return Ok(cliente);
    }
}   
