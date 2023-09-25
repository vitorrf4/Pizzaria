using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("[controller]")]
public class PedidoFinalController : ControllerBase
{
    private readonly ILogger<PedidoFinalController> _logger;
    private PizzariaDBContext _context;

    public PedidoFinalController(PizzariaDBContext context, ILogger<PedidoFinalController> logger) 
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<PedidoFinal>>> Listar()
    {
        if (_context.PedidoFinal is null)
            return NotFound();
        
        return await _context.PedidoFinal.ToListAsync();
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<PedidoFinal>> Buscar([FromRoute] int id)   
    {
        var PedidoFinal = await _context.PedidoFinal.FindAsync(id);
        if (PedidoFinal == null)
            return NotFound();
        
        return Ok(PedidoFinal);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(PedidoFinal pedidoFinal)
    {
        await _context.AddAsync(pedidoFinal);
        await _context.SaveChangesAsync();
        return Created("", pedidoFinal);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar (PedidoFinal pedidoFinal)
    {
        _context.PedidoFinal.Update(pedidoFinal);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir")]
    public async Task<IActionResult> Excluir(int id)
    {
        var pedidoFinal = await _context.PedidoFinal.FindAsync(id);
        if(pedidoFinal is null) return NotFound();
        
        _context.PedidoFinal.Remove(pedidoFinal);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
