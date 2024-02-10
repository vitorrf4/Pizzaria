using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Services;

public class PedidoFinalService : IPedidoFinalService
{
    private readonly PizzariaDbContext _context;

    public PedidoFinalService(PizzariaDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<PedidoFinal>> Listar()
    {
        var pedidoFinal = await GetPedidosFinaisComTodasAsPropriedades()
                                .ToListAsync();

        return pedidoFinal;
    }

    public async Task<PedidoFinal?> BuscarPorId(int id)
    {
        return await GetPedidosFinaisComTodasAsPropriedades()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> Cadastrar(PedidoFinal pedidoFinal)
    {       
        pedidoFinal.CalcularPrecoTotal();
        pedidoFinal.HoraPedido = DateTime.Now;

        MudarTrackingDosCampos(pedidoFinal);

        return await Salvar();
    }

    private void MudarTrackingDosCampos(PedidoFinal pedidoFinal) {
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
    }

    public async Task<bool> Excluir(int id)
    {
        var pedidoFinal = await GetPedidosFinaisComTodasAsPropriedades()
                                .Where(p => p.Id == id)
                                .FirstOrDefaultAsync();
        if (pedidoFinal == null) 
            return false;

        pedidoFinal.Pizzas.ForEach(p => _context.PizzaPedido.RemoveRange(p));
        pedidoFinal.Acompanhamentos.ForEach(a => _context.AcompanhamentoPedido.RemoveRange(a));
        _context.Remove(pedidoFinal);

        return await Salvar();
    }

    private IQueryable<PedidoFinal> GetPedidosFinaisComTodasAsPropriedades()
    {
        return _context.PedidoFinal
            .Include(p => p.Endereco).ThenInclude(e => e.Regiao)
            .Include(p => p.Acompanhamentos).ThenInclude(a => a.Acompanhamento)
            .Include(p => p.Pizzas).ThenInclude(p => p.Tamanho)
            .Include(p => p.Pizzas).ThenInclude(p => p.Sabores);
    }

    private async Task<bool> Salvar()
    { 
        return await _context.SaveChangesAsync() > 0;
    }
}