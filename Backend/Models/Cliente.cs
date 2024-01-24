using System.ComponentModel.DataAnnotations;

namespace pizzaria;

public class Cliente 
{
    [Key]
    public string Cpf { get; set; } = "";
    public string Nome { get; set; } = "";
    public string Telefone { get; set; } = "";
    public Endereco Endereco { get; set; } = new Endereco();

    public Cliente() { }

    public Cliente(string cpf, string nome, string telefone, Endereco endereco)
    {
        Cpf = cpf;
        Nome = nome;
        Telefone = telefone;
        Endereco = endereco;
    }

    public override string ToString()
    {
        return $"CPF: {Cpf} | Nome: {Nome} | Telefone: {Telefone}";
    }
}