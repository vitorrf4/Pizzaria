namespace pizzaria;

public class Cliente 
{
    public string cpf;
    public string nome;
    public string telefone;

    public Cliente() { }

    public Cliente(string cpf, string nome, string telefone)
    {
        this.cpf = cpf;
        this.nome = nome;
        this.telefone = telefone;
    }

    public string Cpf
    {
        get => cpf;
        set => cpf = value;
    }

    public string Nome
    {
        get => nome;
        set => nome = value;
    }

    public string Telefone
    {
        get => telefone;
        set => telefone = value;
    }
}