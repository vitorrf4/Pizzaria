using System.ComponentModel.DataAnnotations;

namespace pizzaria;

public class Sabor 
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public double Preco { get; set; }

    public Sabor() { }

    public Sabor(int id, string nome, double preco)
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