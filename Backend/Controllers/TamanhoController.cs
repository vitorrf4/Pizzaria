using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("tamanho/")]
public class TamanhoController : ControllerBase
{
    private PizzariaDBContext _context;

    public TamanhoController(PizzariaDBContext context) 
    {
        _context = context;
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Tamanho>>> Listar()
    {
        var tamanhos = await _context.Tamanho.ToListAsync();
        
        return tamanhos;
    }

    [HttpGet]
    [Route("listar/{nome}")]
    public async Task<ActionResult<Tamanho>> Buscar([FromRoute] string nome)
    {
        var Tamanho = await _context.Tamanho.FindAsync(nome);
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
    [Route("excluir/{nome}")]
    public async Task<IActionResult> Excluir(string nome)
    {
        var tamanho = await _context.Tamanho.FindAsync(nome);
        if(tamanho == null) 
            return NotFound();
        
        _context.Tamanho.Remove(tamanho);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}
