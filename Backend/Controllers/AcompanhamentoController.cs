using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("[controller]")]
public class AcompanhamentoController : ControllerBase
{
    private readonly ILogger<AcompanhamentoController> _logger;
    private PizzariaDBContext _context;

    public AcompanhamentoController(PizzariaDBContext context, ILogger<AcompanhamentoController> logger) 
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Acompanhamento>>> Listar()
    {
        if (_context.Acompanhamento is null)
            return NotFound();
        
        return await _context.Acompanhamento.ToListAsync();
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<Acompanhamento>> Buscar([FromRoute] int id)   
    {
        var Acompanhamento = await _context.Acompanhamento.FindAsync(id);
        if (Acompanhamento == null)
            return NotFound();
        
        return Ok(Acompanhamento);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(Acompanhamento acompanhamento)
    {
        await _context.AddAsync(acompanhamento);
        await _context.SaveChangesAsync();
        return Created("", acompanhamento);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar (Acompanhamento acompanhamento)
    {
        _context.Acompanhamento.Update(acompanhamento);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir")]
    public async Task<IActionResult> Excluir(int id)
    {
        var acompanhamento = await _context.Acompanhamento.FindAsync(id);
        if (acompanhamento is null) return NotFound();
        
        _context.Acompanhamento.Remove(acompanhamento);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
