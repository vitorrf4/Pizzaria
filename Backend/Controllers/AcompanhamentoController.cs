using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("acompanhamento/")]
public class AcompanhamentoController : ControllerBase
{
    private PizzariaDBContext _context;

    public AcompanhamentoController(PizzariaDBContext context) 
    {
        _context = context;
    }

    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Acompanhamento>>> Listar()
    {
        var acompanhamentos = await _context.Acompanhamento.ToListAsync();
        
        return acompanhamentos;
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<Acompanhamento>> Buscar([FromRoute] int id)   
    {
        var Acompanhamento = await _context.Acompanhamento.FindAsync(id);
        if (Acompanhamento == null) 
            return NotFound();
        
        return Ok(Acompanhamento);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(Acompanhamento acompanhamento)
    {
        await _context.AddAsync(acompanhamento);
        await _context.SaveChangesAsync();

        return Created("", acompanhamento);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar(Acompanhamento acompanhamento)
    {
        _context.Acompanhamento.Update(acompanhamento);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir")]
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
