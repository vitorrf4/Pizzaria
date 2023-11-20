using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("acompanhamento-pedido/")]
public class AcompanhamentoPedidoController : ControllerBase
{
    private PizzariaDBContext _context;

    public AcompanhamentoPedidoController(PizzariaDBContext context) 
    {
        _context = context;
    }

    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<AcompanhamentoPedido>>> Listar()
    {
       var acompPedidos = await _context.AcompanhamentoPedido
                                .Include("Acompanhamento")
                                .ToListAsync();
        
        return acompPedidos;
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<AcompanhamentoPedido>> Buscar([FromRoute] int id)   
    {
        var acompPedido = await _context.AcompanhamentoPedido
                                .Where(acompBanco => acompBanco.Id == id)
                                .Include("Acompanhamento")
                                .FirstOrDefaultAsync();

        if (acompPedido == null)
            return NotFound();
        
        return Ok(acompPedido);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(AcompanhamentoPedido acompanhamentoPedido)
    {
        _context.Acompanhamento.Attach(acompanhamentoPedido.Acompanhamento);
        
        acompanhamentoPedido.CalcularPreco();

        await _context.AddAsync(acompanhamentoPedido);
        await _context.SaveChangesAsync();

        return Created("", acompanhamentoPedido);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar(AcompanhamentoPedido acompanhamentoPedido)
    {
        var acompanhamentoExisteNoBanco = await _context.AcompanhamentoPedido.ContainsAsync(acompanhamentoPedido);
        if (!acompanhamentoExisteNoBanco)
            return NotFound("Acompanhamento pedido nao encontrado");

        _context.Acompanhamento.Attach(acompanhamentoPedido.Acompanhamento);
        acompanhamentoPedido.CalcularPreco();

        _context.AcompanhamentoPedido.Update(acompanhamentoPedido);
        await _context.SaveChangesAsync();
        
        return Ok();
    }

    [HttpDelete]
    [Route("excluir")]
    public async Task<IActionResult> Excluir(int id)
    {
        var acompanhamentoPedido = await _context.AcompanhamentoPedido.FindAsync(id);
        if(acompanhamentoPedido is null) 
            return NotFound();
        
        _context.AcompanhamentoPedido.Remove(acompanhamentoPedido);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
