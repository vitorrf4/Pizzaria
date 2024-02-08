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

    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> BuscarPorCPF(int id)
    {
        var cliente = await _context.Cliente
            .Where(cliente => cliente.Id == id)
            .Include("Endereco.Regiao")
            .FirstOrDefaultAsync();

        if (cliente == null) 
            return NotFound("Nenhum cliente encontrado");
        
        return Ok(cliente);
    }

    [HttpGet("{id}/pedidos")]
    public async Task<IActionResult> ListarPedidosPorCliente([FromRoute] int id)
    {
        var pedidos = await GetPedidosFinaisComTodasAsPropriedades()
            .Where(p => p.ClienteId == id)
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
    [HttpDelete("{id}")]
    public async Task<ActionResult> Deletar(int id)
    {
        var cliente = await _context.Cliente
            .Where(cliente => cliente.Id == id)
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
