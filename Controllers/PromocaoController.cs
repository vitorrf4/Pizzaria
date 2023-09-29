using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("[controller]")]
public class PromocaoController : ControllerBase
{
    private PizzariaDBContext _context;

    public PromocaoController(PizzariaDBContext context)
    {
        _context = context;
    }
     
    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Promocao>>> Listar()
    {
        var promocoes = await _context.Promocao.ToListAsync();
        if (promocoes is null)
            return NotFound();

        return Ok(promocoes);
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<Promocao>> Buscar([FromRoute] int id)
    {
        var promocao = await _context.Promocao.FindAsync(id);
        if (promocao == null) return NotFound();

        return Ok(promocao);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(Promocao promocao)
    {
        await _context.AddAsync(promocao);
        await _context.SaveChangesAsync();
        return Created("", promocao);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar(Promocao promocao)
    {
        _context.Promocao.Update(promocao);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir")]
    public async Task<IActionResult> Excluir(int id)
    {
        var promocao = await _context.Promocao.FindAsync(id);
        if (promocao is null) return NotFound();

        _context.Promocao.Remove(promocao);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
