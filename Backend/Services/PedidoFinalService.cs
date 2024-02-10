using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.DTOs;
using Pizzaria.Models;
using Pizzaria.Services.Interfaces;

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

    public async Task<PedidoFinal?> Cadastrar(PedidoDto pedidoFinal)
    {
        var pedido = await CriarPedido(pedidoFinal);
        if (pedido == null)
            return null;
        // _context.Attach(pedido.Cliente);
        await _context.AddAsync(pedido);

        // MudarTrackingDosCampos(pedido);

        var pedidoFoiSalvo = await Salvar(); 
        return pedidoFoiSalvo ? pedido : null;
    }

    private async Task<PedidoFinal?> CriarPedido(PedidoDto pedidoDto)
    {
        var saboresDb = new List<Sabor>();
        var pizzas = new List<PizzaPedido>();
        
        foreach (var pizza in pedidoDto.Pizzas)
        {
            foreach (var sabor in pizza.Sabores)
            {
                var pizzaDb = await _context.Sabor
                    .Where(p => p.Nome.ToUpper() == sabor.ToUpper())
                    .FirstOrDefaultAsync();

                if (pizzaDb == null)
                    return null;
                
                saboresDb.Add(pizzaDb);
            }

            var tamanhoDb = await _context.Tamanho
                .Where(t => t.Nome.ToUpper() == pizza.Tamanho.ToUpper())
                .FirstOrDefaultAsync();
            if (tamanhoDb == null)
                return null;

            pizzas.Add(new PizzaPedido(saboresDb, tamanhoDb, pizza.Quantidade));
        }

        var clienteDb = await _context.Cliente
            .Where(c => c.Id == pedidoDto.ClienteId)
            .Include(c => c.Endereco).ThenInclude(e => e.Regiao)
            .FirstOrDefaultAsync();
                        
            
        if (clienteDb == null)
            return null;

        return new PedidoFinal(clienteDb, pizzas);
    }

    // private void MudarTrackingDosCampos(PedidoFinal pedidoFinal) {
    //     foreach (var p in pedidoFinal.Pizzas) {
    //         _context.Attach(p);
    //         _context.SaveChanges();
    //         _context.ChangeTracker.Clear();
    //     }
    //
    //     _context.ChangeTracker.TrackGraph(pedidoFinal, p =>
    //     {
    //         if (!p.Entry.IsKeySet)
    //             p.Entry.State = EntityState.Added;                                                                       
    //         else if (p.Entry.Metadata.DisplayName() == "Sabor")
    //             p.Entry.State = EntityState.Detached;
    //         else 
    //             p.Entry.State = EntityState.Unchanged;   
    //     });
    // }

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
            .Include(p => p.Cliente).ThenInclude(c => c.Endereco).ThenInclude(e => e.Regiao)
            .Include(p => p.Acompanhamentos).ThenInclude(a => a.Acompanhamento)
            .Include(p => p.Pizzas).ThenInclude(p => p.Tamanho)
            .Include(p => p.Pizzas).ThenInclude(p => p.Sabores);
    }

    private async Task<bool> Salvar()
    { 
        return await _context.SaveChangesAsync() > 0;
    }
}