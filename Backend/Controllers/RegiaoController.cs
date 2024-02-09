using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Controllers;

[Route("regiao")]
[ApiController]
public class RegiaoController : ControllerBase
{
    private readonly PizzariaDbContext _context;

    public RegiaoController(PizzariaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Regiao>>> ListarTodos()
    {   
        var regioes = await _context.Regiao.ToListAsync();

        return Ok(regioes); 
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Regiao>> BuscarPorId([FromRoute] int id)
    {
        var regiao = await _context.Regiao.FindAsync(id);

        if (regiao == null) 
            return NotFound();

        return Ok(regiao);
    }

    [HttpPost]
    public async Task<ActionResult<Regiao>> Cadastrar(Regiao regiao)
    {
        if (_context.Regiao.Contains(regiao))
            return Conflict();

        await _context.AddAsync(regiao);
        await _context.SaveChangesAsync(); 

        return Created("", regiao);
    }

    [HttpPut]
    public async Task<ActionResult> Alterar(Regiao regiao)
    {
        if (!_context.Regiao.Contains(regiao)) 
            return NotFound();

        _context.Regiao.Update(regiao);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Deletar(int id)
    {
        var regiao = await _context.Regiao.FindAsync(id);

        if (regiao == null) 
            return NotFound();

        _context.Regiao.Remove(regiao);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}