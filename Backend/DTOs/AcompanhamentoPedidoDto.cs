using JetBrains.Annotations;

namespace Pizzaria.DTOs;

[PublicAPI]
public struct AcompanhamentoPedidoDto
{
    public string Acompanhamento { get; set; }
    public int Quantidade { get; set; }

    public AcompanhamentoPedidoDto(string acompanhamento, int quantidade)
    {
        Acompanhamento = acompanhamento;
        Quantidade = quantidade;
    }
}