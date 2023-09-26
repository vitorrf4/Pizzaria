using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("[controller]")]
public class TamanhoController : ControllerBase
{
    private readonly ILogger<TamanhoController> _logger;
    private PizzariaDBContext _context;

    public TamanhoController(PizzariaDBContext context, ILogger<TamanhoController> logger) 
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Tamanho>>> Listar()
    {
        if (_context.Tamanho is null)
            return NotFound();
        
        return await _context.Tamanho.ToListAsync();
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<Tamanho>> Buscar([FromRoute] int id)   
    {
        var Tamanho = await _context.Tamanho.FindAsync(id);
        if (Tamanho == null)
            return NotFound();
        
        return Ok(Tamanho);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(Tamanho tamanho)
    {
        await _context.AddAsync(tamanho);
        await _context.SaveChangesAsync();
        return Created("", tamanho);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar (Tamanho tamanho)
    {
        _context.Tamanho.Update(tamanho);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir")]
    public async Task<IActionResult> Excluir(int id)
    {
        var tamanho = await _context.Tamanho.FindAsync(id);
        if(tamanho is null) return NotFound();
        
        _context.Tamanho.Remove(tamanho);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
