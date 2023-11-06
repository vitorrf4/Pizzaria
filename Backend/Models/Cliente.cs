using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzaria;

public class Cliente 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public DateOnly DataAniversario { get; set; }
    public Endereco Endereco { get; set; }

    public Cliente() { }

    public Cliente(string cpf, string nome, string telefone, DateOnly dataAniversario, Endereco endereco)
    {
        Cpf = cpf;
        Nome = nome;
        Telefone = telefone;
        DataAniversario = dataAniversario;
        Endereco = endereco;
    }

    public override string ToString()
    {
        return $"CPF: {Cpf} | Nome: {Nome} | Telefone: {Telefone} | DataAniversario Aniversário: {DataAniversario}";
    }
}