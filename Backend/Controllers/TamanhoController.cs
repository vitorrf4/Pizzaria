using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Controllers;

[ApiController]
[Route("tamanho/")]
public class TamanhoController : ControllerBase
{
    private readonly PizzariaDbContext _context;

    public TamanhoController(PizzariaDbContext context) 
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tamanho>>> Listar()
    {
        var tamanhos = await _context.Tamanho.ToListAsync();
        
        return tamanhos;
    }

    [HttpGet("{nome}")]
    public async Task<ActionResult<Tamanho>> Buscar([FromRoute] string nome)
    {
        var tamanho = await _context.Tamanho.FindAsync(nome);
        if (tamanho == null)
            return NotFound();
        
        return Ok(tamanho);
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(Tamanho tamanho)
    {
        await _context.AddAsync(tamanho);
        await _context.SaveChangesAsync();

        return Created("", tamanho);
    }

    [HttpPut]
    public async Task<IActionResult> Alterar (Tamanho tamanho)
    {
        _context.Tamanho.Update(tamanho);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{nome}")]
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
