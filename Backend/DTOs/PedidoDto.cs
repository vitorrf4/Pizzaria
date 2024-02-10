namespace Pizzaria.DTOs;

public struct PedidoDto
{
    public int ClienteId { get; set; }
    public List<PizzaPedidoDto> Pizzas { get; init; }
    public Dictionary<string, int>? Acompanhamentos { get; init; }
     
    public PedidoDto(int clienteId, List<PizzaPedidoDto> pizzas,  
                    Dictionary<string, int>? acompanhamentos) 
    {
        ClienteId = clienteId;
        Pizzas = pizzas;
        Acompanhamentos = acompanhamentos;
    }
}  