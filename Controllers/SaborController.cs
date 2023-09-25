using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("[controller]")]
public class SaborController : ControllerBase
{
    private readonly ILogger<SaborController> _logger;
    private PizzariaDBContext _context;

    public SaborController(PizzariaDBContext context, ILogger<SaborController> logger) 
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Sabor>>> Listar()
    {
        if (_context.Sabor is null)
            return NotFound();
        
        return await _context.Sabor.ToListAsync();
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<string>> Buscar([FromRoute] int id)   
    {
        var Sabor = await _context.Sabor.FindAsync(id);
        if (Sabor == null)
            return NotFound();
        
        return Sabor.ToString();
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(Sabor sabor)
    {
        await _context.AddAsync(sabor);
        await _context.SaveChangesAsync();
        return Created("", sabor);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar (Sabor sabor)
    {
        _context.Sabor.Update(sabor);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir")]
    public async Task<IActionResult> Excluir(int id)
    {
        var sabor = await _context.Sabor.FindAsync(id);
        if(sabor is null) return NotFound();
        _context.Sabor.Remove(sabor);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
