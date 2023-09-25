using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pizzaria;

public class AcompanhamentoPedido
{
    [Key]
    public int Id { get; set; }
    public Acompanhamento Acompanhamento { get; set; }
    public int Quantidade { get; set; }
    public double PrecoTotal { get; set; }
    [JsonIgnore]
    public List<PedidoFinal>? PedidosFinais { get; set; }

    public AcompanhamentoPedido() { }

    public AcompanhamentoPedido(int id, Acompanhamento acompanhamento, int quantidade)
    {
        Id = id;
        Acompanhamento = acompanhamento;
        Quantidade = quantidade;
        PrecoTotal = acompanhamento.Preco * quantidade;
    }

    public override string ToString()
    {
        Console.Write($"Acompanhamento: {Acompanhamento.Nome} | ");
        Console.Write($"Preço Unitário: R${Acompanhamento.Preco} | ");
        Console.Write($"Quantidade: {Quantidade} | ");
        Console.Write($"Preço Total do Acompanhamento: R${PrecoTotal}");
        return "";
    }
}