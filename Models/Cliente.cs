namespace pizzaria;

public class Cliente 
{
    private string _cpf;
    private string _nome;
    private string _telefone;

    public Cliente() { }

    public Cliente(string cpf, string nome, string telefone)
    {
        this._cpf = cpf;
        this._nome = nome;
        this._telefone = telefone;
    }

    public string Cpf
    {
        get => _cpf;
        set => _cpf = value;
    }

    public string Nome
    {
        get => _nome;
        set => _nome = value;
    }

    public string Telefone
    {
        get => _telefone;
        set => _telefone = value;
    }

    public override string ToString()
    {
        return $"CPF: {_cpf} | Nome: {_nome} | Telefone: {_telefone}";
    }
}