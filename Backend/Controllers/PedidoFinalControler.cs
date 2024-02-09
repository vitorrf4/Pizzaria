using Microsoft.AspNetCore.Mvc;
using Pizzaria.Models;
using Pizzaria.Services;

namespace Pizzaria.Controllers;

[ApiController]
[Route("pedido-final/")]
public class PedidoFinalController : ControllerBase
{
    private readonly PedidoFinalService _service;

    public PedidoFinalController(PedidoFinalService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoFinal>>> Listar()
    {
        var pedidos = await _service.Listar();

        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoFinal>> Buscar([FromRoute] int id)
    {
        var pedidoFinal = await _service.BuscarPorId(id);
        
        return pedidoFinal != null ? Ok(pedidoFinal) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(PedidoFinal pedidoFinal)
    {
        var foiCadastrado = await _service.Cadastrar(pedidoFinal);

        return foiCadastrado ? Created($"/{pedidoFinal.Id}", pedidoFinal) : BadRequest();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir([FromRoute] int id)
    {
        var foiDeletado = await _service.Excluir(id);
        return foiDeletado ? NoContent() : NotFound();  
    }
}
