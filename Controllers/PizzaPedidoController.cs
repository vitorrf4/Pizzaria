using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[Route("[controller]")]
[ApiController]
public class PizzaPedidoController : ControllerBase
{
    private readonly ILogger<ClienteController> _logger;
    public PizzariaDBContext _context;

    public PizzaPedidoController(PizzariaDBContext context, ILogger<ClienteController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet()]
    [Route("buscar")]
    public async Task<ActionResult<IEnumerable<Cliente>>> ListarTodos()
    {   
        try
        {
            List<PizzaPedido> pedidos;
            pedidos = await _context.PizzaPedido.Include("Tamanho").Include("Sabores").ToListAsync();
            return Ok(pedidos); 
        }
        catch (SqliteException)
        {
            return NotFound("A tabela PizzaPedido não existe");
        }
    }

    [HttpGet()]
    [Route("buscar/{id}")]
    public async Task<ActionResult<PizzaPedido>> BuscarPorId(string id)
    {
        if (!int.TryParse(id, out int idInt)) return BadRequest();

        var pedido = await _context.PizzaPedido
            .Where(pedido => pedido.Id == idInt)
            .Include("Tamanho").Include("Sabores")
            .FirstOrDefaultAsync();

        if (pedido == null) return NotFound($"Nenhum pedido com o ID {idInt} encontrado");

        return Ok(pedido);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult<PizzaPedido>> Cadastrar(PizzaPedido pedido)
    {
        var saboresPedido = new List<Sabor>();

        // procura o sabor no banco e relaciona com o pedido
        pedido.Sabores.ForEach(sabor => saboresPedido.Add(_context.Sabor.Find(sabor.Id)));
        pedido.Tamanho = _context.Tamanho.Find(pedido.Tamanho.Nome);

        if (pedido.Tamanho == null || saboresPedido.Any(sabor => sabor == null)) return BadRequest();

        pedido.Sabores = saboresPedido;
        pedido.CalcularPreco();
        await _context.AddAsync(pedido);
        await _context.SaveChangesAsync(); 

        return Created("", pedido);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(PizzaPedido pedido)
    {
        _context.PizzaPedido.Update(pedido);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("deletar/{id}")]
    public async Task<ActionResult> Deletar(string id)
    {
        if (!int.TryParse(id, out int idInt)) return BadRequest();

        var pedido = await _context.PizzaPedido.FindAsync(idInt);

        if (pedido == null) return NotFound($"Nenhum pedido com o ID {idInt} encontrado");

        _context.PizzaPedido.Remove(pedido);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

