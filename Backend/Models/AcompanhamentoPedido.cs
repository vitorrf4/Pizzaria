using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pizzaria;

public class AcompanhamentoPedido
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Acompanhamento Acompanhamento { get; set; }
    public int Quantidade { get; set; }
    public double PrecoTotal { get; private set; }
    [JsonIgnore]
    public PedidoFinal? PedidoFinal { get; set; }

    public AcompanhamentoPedido() { }

    public AcompanhamentoPedido(Acompanhamento acompanhamento, int quantidade)
    {
        Acompanhamento = acompanhamento;
        Quantidade = quantidade;
        CalcularPreco();
    }

    public void CalcularPreco()
    {
        PrecoTotal = Acompanhamento.Preco * Quantidade;
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