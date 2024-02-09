using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Controllers;

[ApiController]
[Route("endereco/")]
public class EnderecoController : ControllerBase
{
    private readonly PizzariaDbContext _context;

    public EnderecoController(PizzariaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Endereco>>> Listar()
    {
        var enderecos = await _context.Endereco
            .Include("Regiao")
            .ToListAsync();

        return Ok(enderecos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Endereco>> Buscar([FromRoute] int id)
    {
        var endereco = await _context.Endereco
            .Where(e => e.Id == id)
            .Include("Regiao")
            .FirstOrDefaultAsync();

        if (endereco == null) 
            return NotFound($"Nenhum endereço com o id {id} encontrado");

        return Ok(endereco);
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(Endereco endereco)
    {
        var regiaoDb = await _context.Regiao
            .Where(r => r.Nome == endereco.Regiao.Nome)
            .FirstOrDefaultAsync();

        if (regiaoDb != null) 
            endereco.Regiao = regiaoDb;

        await _context.AddRangeAsync(endereco);
        await _context.SaveChangesAsync();

        return Created("", endereco);
    }
 
    [HttpPut]
    public async Task<IActionResult> Alterar(Endereco endereco)
    {
        _context.Endereco.Update(endereco);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var endereco = await _context.Endereco.FindAsync(id);
        if (endereco == null) 
            return NotFound($"Nenhum endereço com o id {id} encontrado");
         
        _context.Endereco.Remove(endereco);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
