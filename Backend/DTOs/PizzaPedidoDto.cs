using JetBrains.Annotations;

namespace Pizzaria.DTOs;

[PublicAPI]
public struct PizzaPedidoDto
{
    public List<string> Sabores { get; set; } = new();
    public string Tamanho { get; set; }
    public int Quantidade { get; set; }

    public PizzaPedidoDto(List<string> sabores, string tamanho, int quantidade)
    {
        Sabores = sabores;
        Tamanho = tamanho;
        Quantidade = quantidade;
    }
}