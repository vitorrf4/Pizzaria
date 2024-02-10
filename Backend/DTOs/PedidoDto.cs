using Pizzaria.Models;

namespace Pizzaria.DTOs;

public struct PedidoDto
{
    public int ClienteId { get; set; }
    public List<PizzaPedidoDto> Pizzas { get; init; }
    public Dictionary<string, string>? Acompanhamentos { get; init; }
    
    public PedidoDto(int clienteId, List<PizzaPedidoDto> pizzas,  
                    Dictionary<string, string>? acompanhamentos) 
    {
        ClienteId = clienteId;
        Pizzas = pizzas;
        Acompanhamentos = acompanhamentos;
    }
}  