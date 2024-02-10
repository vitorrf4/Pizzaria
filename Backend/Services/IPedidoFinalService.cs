using Pizzaria.Models;

namespace Pizzaria.Services;

public interface IPedidoFinalService
{
    public Task<IEnumerable<PedidoFinal>> Listar();
    public Task<PedidoFinal?> BuscarPorId(int id);
    public Task<bool> Cadastrar(PedidoFinal pedidoFinal);
    public Task<bool> Excluir(int id);
}