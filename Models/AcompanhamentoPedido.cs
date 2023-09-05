namespace pizzaria;

public class AcompanhamentoPedido
{
    private Acompanhamento acompanhamento;
    private int quantidade;

    public AcompanhamentoPedido() { }

    public AcompanhamentoPedido(Acompanhamento acompanhamento, int quantidade)
    {
        this.acompanhamento = acompanhamento;
        this.quantidade = quantidade;
    }

    public Acompanhamento Acompanhamento
    {
        get => acompanhamento;
        set => acompanhamento = value;
    }
    
    public int Quantidade
    {
        get => quantidade;
        set => quantidade = value;
    }
}