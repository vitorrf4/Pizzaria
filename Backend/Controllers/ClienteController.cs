using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController] 
[Route("cliente/")]
public class ClienteController : ControllerBase
{
    private readonly PizzariaDBContext _context;

    public ClienteController(PizzariaDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> ListarTodos()
    {
        var clientes = await _context.Cliente
                        .Include("Endereco.Regiao")
                        .ToListAsync(); 

        return Ok(clientes);
    }

    [HttpGet("{cpf}")]
    public async Task<ActionResult<Cliente>> BuscarPorCPF(string cpf)
    {
        var cliente = await _context.Cliente
            .Where(cliente => cliente.Cpf == cpf)
            .Include("Endereco.Regiao")
            .FirstOrDefaultAsync();

        if (cliente == null) 
            return NotFound("Nenhum cliente encontrado");
        
        return Ok(cliente);
    }

    [HttpGet("{cpf}/pedidos")]
    public async Task<IActionResult> ListarPedidosPorCliente([FromRoute] string cpf)
    {
        var pedidos = await GetPedidosFinaisComTodasAsPropriedades()
            .Where(p => p.ClienteCpf == cpf)
            .ToListAsync();

        return Ok(pedidos);
    }

    [HttpPost]
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

    [HttpPut]
    public async Task<ActionResult> Alterar(Cliente cliente)
    {
        _context.Cliente.Update(cliente);
        await _context.SaveChangesAsync();

        return Ok();
    }

    // FIX: excluir nao funciona devido ao relacionamento
    [HttpDelete("{cpf}")]
    public async Task<ActionResult> Deletar(string cpf)
    {
        var cliente = await _context.Cliente
            .Where(cliente => cliente.Cpf == cpf)
            .Include(c => c.Endereco)
            .FirstOrDefaultAsync();

        if (cliente == null) 
            return NotFound("Cliente não encontrado");

        _context.Endereco.Remove(cliente.Endereco);
        _context.Cliente.Remove(cliente);

        await _context.SaveChangesAsync();
        return NoContent();
    }

    private IQueryable<PedidoFinal> GetPedidosFinaisComTodasAsPropriedades()
    {
        return _context.PedidoFinal
            .Include(p => p.Endereco)
            .Include(p => p.Acompanhamentos).ThenInclude(a => a.Acompanhamento)
            .Include(p => p.Pizzas).ThenInclude(p => p.Tamanho)
            .Include(p => p.Pizzas).ThenInclude(p => p.Sabores);
    }
}
