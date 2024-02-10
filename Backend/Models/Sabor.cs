using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Pizzaria.Models;

[PublicAPI]
public class Sabor 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "O nome não deve ultrapassar 100 caracteres")]
    public string Nome { get; set; } = "";
    [Required(ErrorMessage = "O preço é obrigatório")]
    [Range(0, 1000, ErrorMessage = "O preço deve estar entre 0 e 1000")]
    public double Preco { get; set; }

    private Sabor() { }

    public Sabor(string nome, double preco)
    {
        Nome = nome;
        Preco = preco;
    }

    public override string ToString()
    {
        return $"Nome: {Nome} | Preço: {Preco}";
    }
}