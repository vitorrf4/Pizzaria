using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Controllers;

[ApiController]
[Route("acompanhamento/")]
public class AcompanhamentoController : ControllerBase
{
    private readonly PizzariaDbContext _context;

    public AcompanhamentoController(PizzariaDbContext context) 
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Acompanhamento>>> Listar()
    {
        var acompanhamentos = await _context.Acompanhamento.ToListAsync();
        
        return acompanhamentos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Acompanhamento>> Buscar([FromRoute] int id)   
    {
        var acompanhamento = await _context.Acompanhamento.FindAsync(id);
        if (acompanhamento == null) 
            return NotFound();
        
        return Ok(acompanhamento);
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(Acompanhamento acompanhamento)
    {
        await _context.AddAsync(acompanhamento);
        await _context.SaveChangesAsync();

        return Created("", acompanhamento);
    }

    [HttpPut]
    public async Task<IActionResult> Alterar(Acompanhamento acompanhamento)
    {
        _context.Acompanhamento.Update(acompanhamento);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var acompanhamento = await _context.Acompanhamento.FindAsync(id);
        if (acompanhamento == null) 
            return NotFound();
        
        _context.Acompanhamento.Remove(acompanhamento);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
