using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController] 
[Route("cliente/")]
public class ClienteController : ControllerBase
{
    private PizzariaDBContext _context;

    public ClienteController(PizzariaDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Cliente>>> ListarTodos()
    {
        var clientes = await _context.Cliente
                        .Include("Endereco.Regiao")
                        .ToListAsync(); 

        return Ok(clientes);
    }

    [HttpGet]
    [Route("listar/{cpf}")]
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

    [HttpGet]
    [Route("{cpf}/pedidos")]
    public async Task<IActionResult> ListarPedidosPorCliente([FromRoute] string cpf)
    {
        var pedidos = await GetPedidosFinaisComTodasAsPropriedades()
            .Where(p => p.Cliente.Cpf == cpf)
            .ToListAsync();

        if (pedidos.Count == 0) 
            return NotFound("Nenhum pedido encontrado");

        return Ok(pedidos);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult<Cliente>> Cadastrar(Cliente cliente)
    {
        if (_context.Cliente.Contains(cliente)) 
            return Conflict("Um cliente com esse CPF já está cadastrado");

        if (cliente.Endereco == null) 
            return BadRequest("Endereço inválido");

        await _context.AddAsync(cliente);
        await _context.SaveChangesAsync();

        return Created("", cliente);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Cliente cliente)
    {
        _context.Cliente.Update(cliente);
        await _context.SaveChangesAsync();

        return Ok();
    }

    // FIX: excluir nao funciona devido ao relacionamento
    [HttpDelete]
    [Route("excluir/{cpf}")]
    public async Task<ActionResult> Deletar(string cpf)
    {
        var cliente = await _context.Cliente
            .Where(cliente => cliente.Cpf == cpf)
            .Include(c => c.Endereco)
            .FirstOrDefaultAsync();

        if (cliente == null) 
            return NotFound("Cliente não encontrado");

        if (cliente.Endereco != null) 
            _context.Endereco.Remove(cliente.Endereco);

        _context.Cliente.Remove(cliente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
 
    private IQueryable<PedidoFinal> GetPedidosFinaisComTodasAsPropriedades()
    {
        return _context.PedidoFinal
            .Include(p => p.Cliente.Endereco.Regiao)
            .Include(p => p.Acompanhamentos).ThenInclude(a => a.Acompanhamento)
            .Include(p => p.Pizzas).ThenInclude(p => p.Tamanho)
            .Include(p => p.Pizzas).ThenInclude(p => p.Sabores)
            .Include(p => p.Promocao);
    }
}
