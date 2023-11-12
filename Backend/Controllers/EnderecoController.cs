using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("endereco/")]
public class EnderecoController : ControllerBase
{
    private PizzariaDBContext _context;

    public EnderecoController(PizzariaDBContext context)
    {
        _context = context;
    }

    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Endereco>>> Listar()
    {
        var enderecos = await _context.Endereco
            .Include("Regiao")
            .ToListAsync();

        return Ok(enderecos);
    }

    [HttpGet]
    [Route("listar/{id}")]
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
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(Endereco endereco)
    {
        await _context.AddRangeAsync(endereco);
        await _context.SaveChangesAsync();

        return Created("", endereco);
    }
 
    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar(Endereco endereco)
    {
        _context.Endereco.Update(endereco);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete]
    [Route("excluir")]
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
