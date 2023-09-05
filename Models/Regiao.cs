namespace pizzaria;

public class Regiao
{
    private int id;
    private string nome;
    private string preco;

    public Regiao() { }

    public Regiao(int id, string nome, string preco)
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

    public string Preco
    {
        get => preco;
        set => preco = value;
    }

}