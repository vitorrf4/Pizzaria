namespace pizzaria;

public class Tamanho
{
    private string nome;
    private int qntdFatias;
    private double preco;

    public Tamanho() { }

    public Tamanho(string nome, int qntdFatias,double preco) 
    { 
        this.nome = nome;
        this.qntdFatias = qntdFatias;
        this.preco = preco;
    }

    public string Nome
    {
        get => nome;
        set => this.nome = value;
    }

    public int QntdFatias
    {
        get => qntdFatias;
        set => this.qntdFatias = value;
    }

    public double Preco
    {
        get => preco;
        set => this.preco = value;
    }
}