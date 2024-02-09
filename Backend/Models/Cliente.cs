namespace Pizzaria.Models;

public class Cliente : Usuario
{
    public string Nome { get; set; } = "";
    public string Telefone { get; set; } = "";
    public Endereco Endereco { get; set; } = new();

    public Cliente() { }

    public Cliente(string email, string senha, string nome, 
                    string telefone, Endereco endereco) : base(email, senha)
    {
        Nome = nome;
        Telefone = telefone;
        Endereco = endereco;
    }

    public override string ToString()
    {
        return $"Email: {Email} | Nome: {Nome} | Telefone: {Telefone}";
    }
}