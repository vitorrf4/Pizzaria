using Pizzaria.DTOs;
using Pizzaria.Models;

namespace Pizzaria.Services.Interfaces;

public interface IPedidoFinalService
{
    public Task<IEnumerable<PedidoFinal>> Listar();
    public Task<PedidoFinal?> BuscarPorId(int id);
    public Task<Resultado<PedidoFinal>> CriarPedido(PedidoFinalDto pedidoFinalFinal);
    public Task<bool> SalvarPedido(PedidoFinal pedidoFinal);
    public Task<bool> Excluir(int id);
} 