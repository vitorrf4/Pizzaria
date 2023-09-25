using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("[controller]")]
public class AcompanhamentoPedidoController : ControllerBase
{
    private readonly ILogger<AcompanhamentoPedidoController> _logger;
    private PizzariaDBContext _context;

    public AcompanhamentoPedidoController(PizzariaDBContext context, ILogger<AcompanhamentoPedidoController> logger) 
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<AcompanhamentoPedido>>> Listar()
    {
        if (_context.AcompanhamentoPedido is null)
            return NotFound();
        
        return await _context.AcompanhamentoPedido.ToListAsync();
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<AcompanhamentoPedido>> Buscar([FromRoute] int id)   
    {
        var AcompanhamentoPedido = await _context.AcompanhamentoPedido.FindAsync(id);
        if (AcompanhamentoPedido == null)
            return NotFound();
        
        return Ok(AcompanhamentoPedido);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(AcompanhamentoPedido acompanhamentoPedido)
    {
        await _context.AddAsync(acompanhamentoPedido);
        await _context.SaveChangesAsync();
        return Created("", acompanhamentoPedido);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar (AcompanhamentoPedido acompanhamentoPedido)
    {
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
