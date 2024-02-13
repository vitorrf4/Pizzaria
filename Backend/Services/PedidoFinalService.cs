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
            .AsNoTracking()
            .ToListAsync();

        return pedidoFinal;
    }

    public async Task<PedidoFinal?> BuscarPorId(int id)
    {
        return await GetPedidosFinaisComTodasAsPropriedades()
            .Where(p => p.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<bool> SalvarPedido(PedidoFinal pedidoFinal)
    {
        await _context.PedidoFinal.AddAsync(pedidoFinal);
        return  await Salvar();
    }
    
    public async Task<Resultado<PedidoFinal>> CriarPedido(PedidoFinalDto pedidoFinalDto)
    {
        var clienteDb = await _context.Cliente
            .Where(c => c.Id == pedidoFinalDto.ClienteId)
            .Include(c => c.Endereco).ThenInclude(e => e.Regiao)
            .FirstOrDefaultAsync();
        if (clienteDb == null)
            return Resultado<PedidoFinal>.Falha("Cliente não encontrado");

        var pizzas = new List<PizzaPedido>();
        
        foreach (var pizza in pedidoFinalDto.Pizzas)
        {
            var sabores = new List<Sabor>();
            foreach (var sabor in pizza.Sabores)
            {
                var saborDb = await _context.Sabor
                    .Where(p => p.Nome == sabor)
                    .FirstOrDefaultAsync();
                if (saborDb == null)
                    return Resultado<PedidoFinal>.Falha($"Sabor {sabor} não encontrado");
                
                sabores.Add(saborDb);
            }
            
            var tamanhoDb = await _context.Tamanho
                .Where(t => t.Nome == pizza.Tamanho)
                .FirstOrDefaultAsync();
            if (tamanhoDb == null)
                return Resultado<PedidoFinal>.Falha($"Tamanho {pizza.Tamanho} não encontrado");
            if (pizza.Sabores.Count > tamanhoDb.MaxSabores)
                return Resultado<PedidoFinal>.Falha(
                    "Quantidade de sabores excede limite de sabores do tamanho");

            pizzas.Add(new PizzaPedido(sabores, tamanhoDb, pizza.Quantidade));
        }

        var pedido = new PedidoFinal(clienteDb, pizzas);
        if (pedidoFinalDto.Acompanhamentos == null)
            return Resultado<PedidoFinal>.Sucesso(pedido);
            
        var acompanhamentos = new List<AcompanhamentoPedido>();
        foreach (var acomp in pedidoFinalDto.Acompanhamentos)
        {
            var acompDb = await _context.Acompanhamento
                .Where(a => a.Nome == acomp.Acompanhamento)
                .FirstOrDefaultAsync();
            if (acompDb == null)
                return Resultado<PedidoFinal>.Falha(
                    $"Acompanhamento {acomp.Acompanhamento} não encontrado");
            
            acompanhamentos.Add(new AcompanhamentoPedido(acompDb, acomp.Quantidade));
        }

        pedido.Acompanhamentos = acompanhamentos;
        return Resultado<PedidoFinal>.Sucesso(pedido);
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