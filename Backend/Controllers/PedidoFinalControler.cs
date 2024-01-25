using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("pedido-final/")]
public class PedidoFinalController : ControllerBase
{
    private readonly PizzariaDBContext _context;

    public PedidoFinalController(PizzariaDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoFinal>>> Listar()
    {
        var pedidoFinal = await GetPedidosFinaisComTodasAsPropriedades()
                                .ToListAsync();

        return pedidoFinal;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoFinal>> Buscar([FromRoute] int id)
    {
        var pedidoFinal = await GetPedidosFinaisComTodasAsPropriedades()
                                .Where(p => p.Id == id)
                                .FirstOrDefaultAsync();

        if (pedidoFinal == null)
            return NotFound();

        return Ok(pedidoFinal);
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(PedidoFinal pedidoFinal)
    {       
        pedidoFinal.CalcularPrecoTotal();
        pedidoFinal.HoraPedido = DateTime.Now;

        MudarTrackingDosCampos(pedidoFinal);
        await _context.SaveChangesAsync();

        return Created("", pedidoFinal);
    }

    private bool MudarTrackingDosCampos(PedidoFinal pedidoFinal) {
        foreach (var p in pedidoFinal.Pizzas) {
            _context.Attach(p);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        _context.ChangeTracker.TrackGraph(pedidoFinal, p =>
        {
            if (!p.Entry.IsKeySet)
                p.Entry.State = EntityState.Added;                                                                       
            else if (p.Entry.Metadata.DisplayName() == "Sabor")
                p.Entry.State = EntityState.Detached;
            else 
                p.Entry.State = EntityState.Unchanged;   
        });

        return true;
    }

    [HttpDelete("{id}")]
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
        return _context.PedidoFinal
            .Include(p => p.Endereco)
            .Include(p => p.Acompanhamentos).ThenInclude(a => a.Acompanhamento)
            .Include(p => p.Pizzas).ThenInclude(p => p.Tamanho)
            .Include(p => p.Pizzas).ThenInclude(p => p.Sabores);
    }
}
