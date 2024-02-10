using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Pizzaria.Models;

[PublicAPI]
public class Cliente : Usuario
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MaxLength(30, ErrorMessage = "O nome não deve ultrapassar 30 caracteres")]
    public string Nome { get; set; } = "";
    [Required(ErrorMessage = "O telefone é obrigatório")]
    [MaxLength(11, ErrorMessage = "O telefone não deve ultrapassar 11 caracteres")]
    public string Telefone { get; set; } = "";
    [Required(ErrorMessage = "O endereço é obrigatório")]
    public Endereco Endereco { get; set; }

    private Cliente() { }

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