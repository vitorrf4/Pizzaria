using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using pizzaria;

namespace Pizzaria;

[Route("[controller]")]
[ApiController]
public class RegiaoController : ControllerBase
{
    public PizzariaDBContext _context;

    public RegiaoController(PizzariaDBContext context)
    {
        _context = context;
    }

    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Regiao>>> ListarTodos()
    {   
        try
        {
            List<Regiao> regioes = await _context.Regiao.ToListAsync();
            return Ok(regioes); 
        }
        catch (SqliteException)
        {
            return NotFound("A tabela Região não existe");
        }
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<Regiao>> BuscarPorId(string id)
    {
        if (!int.TryParse(id, out int idInt)) return BadRequest();

        var regiao = await _context.Regiao.FindAsync(idInt);

        if (regiao == null) return NotFound($"Nenhuma região com o ID {idInt} encontrado");

        return Ok(regiao);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult<Regiao>> Cadastrar(Regiao regiao)
    {
        if (_context.Regiao.Contains(regiao)) return Conflict();

        await _context.AddAsync(regiao);
        await _context.SaveChangesAsync(); 

        return Created("", regiao);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Regiao regiao)
    {
        if (!_context.Regiao.Contains(regiao)) return NotFound();

        _context.Regiao.Update(regiao);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("deletar/{id}")]
    public async Task<ActionResult> Deletar(int id)
    {
        var regiao = await _context.Regiao.FindAsync(id);

        if (regiao == null) return NotFound($"Nenhuma região com o ID {id} encontrado");

        _context.Regiao.Remove(regiao);
        await _context.SaveChangesAsync();
        return NoContent();
    }



}