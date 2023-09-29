using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("[controller]")]
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
        if (_context.AcompanhamentoPedido is null)
            return NotFound();
        
        return await _context.AcompanhamentoPedido.Include("Acompanhamento").ToListAsync();
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<AcompanhamentoPedido>> Buscar([FromRoute] int id)   
    {
        var AcompanhamentoPedido = await _context.AcompanhamentoPedido
            .Where(acompBanco => acompBanco.Id == id)
            .Include("Acompanhamento")
            .FirstOrDefaultAsync();

        if (AcompanhamentoPedido == null)
            return NotFound();
        
        return Ok(AcompanhamentoPedido);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(AcompanhamentoPedido acompanhamentoPedido)
    {
        var acompBanco = await _context.Acompanhamento.FindAsync(acompanhamentoPedido.Acompanhamento.Id);
        if (acompBanco == null) return NotFound("Acompanhamento inválido");
        
        acompanhamentoPedido.Acompanhamento = acompBanco;
        acompanhamentoPedido.calcularPreco();

        await _context.AddAsync(acompanhamentoPedido);
        await _context.SaveChangesAsync();
        return Created("", acompanhamentoPedido);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar (AcompanhamentoPedido acompanhamentoPedido)
    {
        if (await _context.AcompanhamentoPedido.FindAsync(acompanhamentoPedido.Id) == null)
            return NotFound("Acompanhamento pedido não encontrado");


        var acompanhamentoBanco = await _context.Acompanhamento.FindAsync(acompanhamentoPedido.Acompanhamento.Id);
        if (acompanhamentoBanco == null) return NotFound("Acompanhamento não encontrado");
        
        acompanhamentoPedido.Acompanhamento = acompanhamentoBanco;
        acompanhamentoPedido.calcularPreco();

        _context.AcompanhamentoPedido.Update(acompanhamentoPedido);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir")]
    public async Task<IActionResult> Excluir(int id)
    {
        var acompanhamentoPedido = await _context.AcompanhamentoPedido.FindAsync(id);
        if(acompanhamentoPedido is null) return NotFound();
        
        _context.AcompanhamentoPedido.Remove(acompanhamentoPedido);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
