using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pizzaria;

public class Sabor 
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public double Preco { get; set; }
    [JsonIgnore]
    public List<PizzaPedido>? Pedidos { get; set; } = new();

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