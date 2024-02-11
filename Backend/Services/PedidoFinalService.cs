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

    public async Task<PedidoFinal?> Cadastrar(PedidoFinalDto pedidoFinalFinal)
    {
        var pedido = await CriarPedido(pedidoFinalFinal);
        if (pedido == null)
            return null;
        
        await _context.AddAsync(pedido);

        var pedidoFoiSalvo = await Salvar(); 
        return pedidoFoiSalvo ? pedido : null;
    }

    private async Task<PedidoFinal?> CriarPedido(PedidoFinalDto pedidoFinalDto)
    {
        //TODO fix database nao acha pedidos com acentos
        var clienteDb = await _context.Cliente
            .Where(c => c.Id == pedidoFinalDto.ClienteId)
            .Include(c => c.Endereco).ThenInclude(e => e.Regiao)
            .FirstOrDefaultAsync();
        if (clienteDb == null)
            return null;
        
        var saboresDb = new List<Sabor>();
        var pizzas = new List<PizzaPedido>();
        
        foreach (var pizza in pedidoFinalDto.Pizzas)
        {
            foreach (var sabor in pizza.Sabores)
            {
                var pizzaDb = await _context.Sabor
                    .Where(p => p.Nome == sabor)
                    .FirstOrDefaultAsync();
                if (pizzaDb == null)
                    return null;
                
                saboresDb.Add(pizzaDb);
            }

            var tamanhoDb = await _context.Tamanho
                .Where(t => t.Nome == pizza.Tamanho)
                .FirstOrDefaultAsync();
            if (tamanhoDb == null)
                return null;

            pizzas.Add(new PizzaPedido(saboresDb, tamanhoDb, pizza.Quantidade));
        }

        var acompanhamentos = new List<AcompanhamentoPedido>();
        foreach (var acomp in pedidoFinalDto.Acompanhamentos)
        {
            var acompDb = await _context.Acompanhamento
                .Where(a => a.Nome == acomp.Acompanhamento)
                .FirstOrDefaultAsync();
            if (acompDb == null)
                return null;
            
            acompanhamentos.Add(new AcompanhamentoPedido(acompDb, acomp.Quantidade));
        }
        
        return new PedidoFinal(clienteDb, pizzas, acompanhamentos);
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