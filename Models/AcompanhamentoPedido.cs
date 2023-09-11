namespace pizzaria;

public class AcompanhamentoPedido
{
    private int _id;
    private Acompanhamento _acompanhamento;
    private int _quantidade;
    private double _precoTotal;

    public AcompanhamentoPedido() { }

    public AcompanhamentoPedido(int id, Acompanhamento acompanhamento, int quantidade)
    {
        _id = id;
        _acompanhamento = acompanhamento;
        _quantidade = quantidade;
        _precoTotal = acompanhamento.Preco * quantidade;
    }

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