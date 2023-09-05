namespace pizzaria;

public class Bairro
{
    private int id;
    private string nome;
    private Regiao area;

    public Bairro() { }

    public Bairro(int id, string nome)
    {
        this.id = id;
        this.nome = nome;
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

    public Regiao Area
    {
        get => area;
        set => area = value;
    }
}