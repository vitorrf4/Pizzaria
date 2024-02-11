using Pizzaria.DTOs;
using Pizzaria.Models;

namespace Pizzaria.Services.Interfaces;

public interface IPedidoFinalService
{
    public Task<IEnumerable<PedidoFinal>> Listar();
    public Task<PedidoFinal?> BuscarPorId(int id);
    public Task<PedidoFinal?> Cadastrar(PedidoFinalDto pedidoFinalFinal);
    public Task<bool> Excluir(int id);
} 