namespace pizzaria;

public class Acompanhamento
{
    private int id;
    private string nome;
    private double preco;

    public Acompanhamento() { }

    public Acompanhamento(int id, string nome, double preco)
    {
        this.id = id;
        this.nome = nome;
        this.preco = preco;
    }

    public int Id
    {
        get => id;
        set => id = value;
    }

    public string Nome
    {
        get => nome;
        set => nome = value;
    }
    
    public double Preco
    {
        get => preco;
        set => preco = value;
    }

}