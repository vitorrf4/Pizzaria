using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("[controller]")]
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

        if (enderecos is null)
            return NotFound();

        return Ok(enderecos);
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<Endereco>> Buscar([FromRoute] int id)
    {
        var endereco = await _context.Endereco
            .Where(enderecoNoBanco => enderecoNoBanco.Id == id)
            .Include("Regiao")
            .FirstOrDefaultAsync();

        if (endereco == null) return NotFound($"Nenhum endereço com o id {id} encontrado");

        return Ok(endereco);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(Endereco endereco)
    {
        var regiaoNoBanco = await _context.Regiao.FindAsync(endereco.Regiao.Id);
//        if (regiaoNoBanco == null) return BadRequest("Regiao invalida");
//        endereco.Regiao = regiaoNoBanco;

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
        if (endereco is null) return NotFound($"Nenhum endereço com o id {id} encontrado");
         
        _context.Endereco.Remove(endereco);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
