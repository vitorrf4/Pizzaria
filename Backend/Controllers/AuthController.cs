using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController] 
[Route("auth/")]
public class AuthController : ControllerBase
{
    private readonly PizzariaDBContext _context;

    public AuthController(PizzariaDBContext context)
    {
        _context = context;
    }

    [HttpPost("/login")]
    public async Task<ActionResult<Cliente>> Login([FromBody] LoginDTO loginDTO)
    {
        var cliente = await _context.Cliente
            .Where(cliente => cliente.Email == loginDTO.Email 
                    && cliente.Senha == loginDTO.Senha)
            .Include(c => c.Endereco).ThenInclude(e => e.Regiao)
            .FirstOrDefaultAsync();

        if (cliente == null) 
            return Unauthorized();
        
        return Ok(cliente);
    }

    [HttpPost("/cadastro")]
    public async Task<ActionResult<Cliente>> Cadastrar(Cliente cliente)
    {
        if (_context.Cliente.Contains(cliente)) 
            return Conflict("Um cliente com esse CPF já está cadastrado");

        VerificaRegiao(cliente);

        await _context.AddAsync(cliente);
        await _context.SaveChangesAsync();

        return Created("", cliente);
    }

    private async void VerificaRegiao(Cliente cliente) 
    {
        var regiaoCliente = cliente.Endereco.Regiao.Nome;

        var regiaoDb = await _context.Regiao
                .Where(r => r.Nome == regiaoCliente)
                .FirstOrDefaultAsync();

        if (regiaoDb != null) 
            cliente.Endereco.Regiao = regiaoDb;
    }
}
