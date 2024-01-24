using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("sabor/")]
public class SaborController : ControllerBase
{
    private PizzariaDBContext _context;

    public SaborController(PizzariaDBContext context) 
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sabor>>> Listar()
    {
        var sabores = await _context.Sabor.ToListAsync();
        
        return Ok(sabores);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sabor>> Buscar([FromRoute] int id)   
    {
        var sabor = await _context.Sabor.FindAsync(id);
        if (sabor == null)
            return NotFound();
        
        return Ok(sabor);
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(Sabor sabor)
    {
        await _context.AddAsync(sabor);
        await _context.SaveChangesAsync();

        return Created("", sabor);
    }

    [HttpPut]
    public async Task<IActionResult> Alterar(Sabor sabor)
    {
        _context.Sabor.Update(sabor);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var sabor = await _context.Sabor.FindAsync(id);
        if(sabor == null)
            return NotFound();
        
        _context.Sabor.Remove(sabor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
