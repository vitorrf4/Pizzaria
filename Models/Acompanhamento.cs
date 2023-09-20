using System.ComponentModel.DataAnnotations;

namespace pizzaria;

public class Acompanhamento
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public double Preco { get; set; }

    public Acompanhamento() { }

    public Acompanhamento(int id, string nome, double preco)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
    }

    public override string ToString()
    {
        return $"ID: {Id} | Nome: {Nome} | Preço: {Preco}";
    }
}