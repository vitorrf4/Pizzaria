using System.ComponentModel.DataAnnotations;

namespace pizzaria;

public class Cliente 
{
    [Key]
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public Endereco Endereco { get; set; }
    public DateOnly DataAniversario { get; set; }


    public Cliente() { }

    public Cliente(string cpf, string nome, string telefone, DateOnly dataAniversario)
    {
        Cpf = cpf;
        Nome = nome;
        Telefone = telefone;
        DataAniversario = dataAniversario;
    }

    public override string ToString()
    {
        return $"CPF: {Cpf} | Nome: {Nome} | Telefone: {Telefone} | DataAniversario Aniversário: {DataAniversario}";
    }
}