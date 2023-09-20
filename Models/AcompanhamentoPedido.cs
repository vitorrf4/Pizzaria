using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pizzaria;

public class AcompanhamentoPedido
{
    private int _id;
    private Acompanhamento _acompanhamento;
    private int _quantidade;
    private double _precoTotal;

    [JsonIgnore]
    public List<PedidoFinal> Pedidos { get; set; }

    public AcompanhamentoPedido() { }

    public AcompanhamentoPedido(Acompanhamento acompanhamento, int quantidade)
    {
        _acompanhamento = acompanhamento;
        _quantidade = quantidade;
        _precoTotal = acompanhamento.Preco * quantidade;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id 
    {
        get => _id;
        set => _id = value;
    }

    public Acompanhamento Acompanhamento
    {
        get => _acompanhamento;
        set => _acompanhamento = value;
    }
    
    public int Quantidade
    {
        get => _quantidade;
        set => _quantidade = value;
    }

    public double PrecoTotal
    {
        get => _precoTotal;
        set => _precoTotal = value;
    }

    public override string ToString()
    {
        Console.Write($"Acompanhamento: {_acompanhamento.Nome} | ");
        Console.Write($"Preço Unitário: R${_acompanhamento.Preco} | ");
        Console.Write($"Quantidade: {_quantidade} | ");
        Console.Write($"Preço Total do Acompanhamento: R${_precoTotal}");
        return "";
    }
}