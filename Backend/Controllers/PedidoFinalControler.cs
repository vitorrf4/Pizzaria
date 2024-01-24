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

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(PedidoFinal pedidoFinal)
    {       
        if (!MudarTrackingDosCampos(pedidoFinal))
            return BadRequest();

        pedidoFinal.CalcularPrecoTotal();
        pedidoFinal.HoraPedido = DateTime.Now;

        await _context.PedidoFinal.AddAsync(pedidoFinal);
        await _context.SaveChangesAsync();

        return Created("", pedidoFinal);
    }

    private bool MudarTrackingDosCampos(PedidoFinal pedidoFinal) {
        _context.ChangeTracker.TrackGraph(pedidoFinal, p =>
        {
            if (!p.Entry.IsKeySet)
                p.Entry.State = EntityState.Added;                                                                       
            else
                p.Entry.State = EntityState.Detached;
        });

        // Para que as pizzas se juntem ao pedido final Precisamos fazer 
        // outro loop para deixar as pizzas como Unchanged ao inves de detached.
        // Ainda verificamos se alguma das pizzas tem um id invalido, se sim
        // retornamos um false aqui e um erro 400 para o usuario na funcao principal
        foreach (var p in  pedidoFinal.Pizzas) 
        {
            if (p.Id <= 0)
                return false;

            _context.Entry(p).State = EntityState.Unchanged;
        }

        return true;
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
        return _context.PedidoFinal
            .Include(p => p.Cliente.Endereco.Regiao)
            .Include(p => p.Acompanhamentos).ThenInclude(a => a.Acompanhamento)
            .Include(p => p.Pizzas).ThenInclude(p => p.Tamanho)
            .Include(p => p.Pizzas).ThenInclude(p => p.Sabores)
            .Include(p => p.Promocao);
    }
}
