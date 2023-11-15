using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("pedido-final/")]
public class PedidoFinalController : ControllerBase
{
    private PizzariaDBContext _context;

    public PedidoFinalController(PizzariaDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<PedidoFinal>>> Listar()
    {
        var pedidoFinal = await GetPedidosFinaisComTodasAsPropriedades()
                                .ToListAsync();

        return pedidoFinal;
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<PedidoFinal>> Buscar([FromRoute] int id)
    {
        var pedidoFinal = await GetPedidosFinaisComTodasAsPropriedades()
                                .Where(p => p.Id == id)
                                .FirstOrDefaultAsync();

        if (pedidoFinal == null)
            return NotFound();

        return Ok(pedidoFinal);
    }

    // FIX: ao tentar cadastrar duas pizzas com o mesmo tamanho ou sabor o programa da erro
    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(PedidoFinal pedidoFinal)
{       
        // A função Attach comunica que um campo já está no banco de dados e não precisa ser inserido novamente
        // sem ela, o entity framework tenta adicionar um campo com um ID existente e da erro
        ChangeTracking(pedidoFinal);

        foreach (var p in  pedidoFinal.Pizzas) {
            if (p.Id <= 0)
                return BadRequest();

            _context.Entry(p).State = EntityState.Unchanged;
        }

        _context.Entry(pedidoFinal.Cliente).State = EntityState.Unchanged;

        pedidoFinal.CalcularPrecoTotal();
        pedidoFinal.HoraPedido = DateTime.Now;

        await _context.PedidoFinal.AddAsync(pedidoFinal);
        await _context.SaveChangesAsync();

        return Created("", pedidoFinal);
    }

    private void ChangeTracking(PedidoFinal pedidoFinal) {
        _context.ChangeTracker.TrackGraph(pedidoFinal, p =>
        {
            if (!p.Entry.IsKeySet)
            {
                p.Entry.State = EntityState.Added;
                System.Console.WriteLine( $" Added: {p.Entry}");
            }
            else
            {
                p.Entry.State = EntityState.Detached;
                System.Console.WriteLine( $" Detached: {p.Entry}");
            }
        });
        System.Console.WriteLine();
    }

    [HttpDelete]
    [Route("excluir/{id}")]
    public async Task<IActionResult> Excluir([FromRoute] int id)
    {
        var pedidoFinal = await GetPedidosFinaisComTodasAsPropriedades()
                                .Where(p => p.Id == id)
                                .FirstOrDefaultAsync();
        if (pedidoFinal == null) 
            return NotFound();

        pedidoFinal.Pizzas.ForEach(p => _context.PizzaPedido.RemoveRange(p));
        pedidoFinal.Acompanhamentos?.ForEach(a => _context.AcompanhamentoPedido.RemoveRange(a));
        _context.Remove(pedidoFinal);
        
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private IQueryable<PedidoFinal> GetPedidosFinaisComTodasAsPropriedades()
    {
        // Campos que sao objetos nao sao retornados automaticamente do banco,
        // precisamos do Include() para que eles sejam incluidos
        return _context.PedidoFinal
            .Include(p => p.Cliente.Endereco.Regiao)
            .Include(p => p.Acompanhamentos).ThenInclude(a => a.Acompanhamento)
            .Include(p => p.Pizzas).ThenInclude(p => p.Tamanho)
            .Include(p => p.Pizzas).ThenInclude(p => p.Sabores)
            .Include(p => p.Promocao);
    }
}
