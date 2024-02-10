using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Pizzaria.Models;

[PublicAPI]
public class Acompanhamento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "O nome não deve ultrapassar 100 caracteres")]
    public string Nome { get; set; } = "";
    [Required(ErrorMessage = "O preço é obrigatório")]
    [Range(0, 1000, ErrorMessage = "O preço deve estar entre 0 e 1000")]
    public double Preco { get; init; }
    
    private Acompanhamento() { } 

    public Acompanhamento(string nome, double preco)
    {
        Nome = nome;
        Preco = preco;
    }
    
    public override string ToString()
    {
        return $"ID: {Id} | Nome: {Nome} | Preço: {Preco}";
    }
}