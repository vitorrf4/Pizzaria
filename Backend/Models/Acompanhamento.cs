using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzaria.Models;

public class Acompanhamento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public string Nome { get; set; } = "";
    public double Preco { get; init; }

    public Acompanhamento() { } 

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