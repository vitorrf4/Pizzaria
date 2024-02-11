using Microsoft.AspNetCore.Mvc;
using Pizzaria.DTOs;
using Pizzaria.Models;
using Pizzaria.Services.Interfaces;

namespace Pizzaria.Controllers;

[ApiController]
[Route("pedido-final/")]

public class PedidoFinalController : ControllerBase
{
    private readonly IPedidoFinalService _service;
    
    public PedidoFinalController(IPedidoFinalService service)
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
    public async Task<ActionResult> Cadastrar([FromBody] PedidoFinalDto pedidoFinalFinal)
    {
        var pedidoCadastrado = await _service.Cadastrar(pedidoFinalFinal);

        return pedidoCadastrado != null ? Created($"/{pedidoCadastrado.Id}", pedidoCadastrado) : BadRequest();
    }
     
    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir([FromRoute] int id)
    {
        var foiDeletado = await _service.Excluir(id);
        return foiDeletado ? NoContent() : NotFound();  
    }
}
