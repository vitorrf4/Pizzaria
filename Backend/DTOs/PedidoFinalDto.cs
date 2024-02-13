using JetBrains.Annotations;

namespace Pizzaria.DTOs;

[PublicAPI]
public struct PedidoFinalDto
{
    public int ClienteId { get; set; }
    public List<PizzaPedidoDto> Pizzas { get; init; }
    public List<AcompanhamentoPedidoDto>? Acompanhamentos { get; init; } = new();
     
    public PedidoFinalDto(int clienteId, List<PizzaPedidoDto> pizzas) 
    {
        ClienteId = clienteId;
        Pizzas = pizzas;
    }
     
    public PedidoFinalDto(int clienteId, List<PizzaPedidoDto> pizzas, 
        List<AcompanhamentoPedidoDto> acompanhamentos) : this(clienteId, pizzas)
    {
        Acompanhamentos = acompanhamentos;
    }
} 