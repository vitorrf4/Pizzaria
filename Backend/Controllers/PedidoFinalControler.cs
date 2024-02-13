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
        var pedido = await _service.BuscarPorId(id);
        
        return pedido != null ? Ok(pedido) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Cadastrar([FromBody] PedidoFinalDto pedidoDto)
    {
        var resultado = await _service.CriarPedido(pedidoDto);
        if (resultado.TemErros)
            return BadRequest(resultado.Mensagem);

        var pedidoFoiSalvo = await _service.SalvarPedido(resultado.Data!);
        return pedidoFoiSalvo
            ? Created($"/pedido-final/{resultado.Data!.Id}", resultado.Data)
            : StatusCode(500, "Erro ao salvar pedido");
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir([FromRoute] int id)
    {
        var foiDeletado = await _service.Excluir(id);
        return foiDeletado ? NoContent() : NotFound();  
    }
}
