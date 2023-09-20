using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pizzaria;

public class Sabor 
{
    private int _id;
    private string _nome;
    private double _preco;
    [JsonIgnore]
    public List<PizzaPedido> PizzaPedidos { get; set; }

    public Sabor() { }

    public Sabor(string nome, double preco)
    {
        _nome = nome;
        _preco = preco;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string ?Nome { get; set; }
    public double Preco { get; set; }

    //public override string ToString()
    //{
    //    return $"ID: {_id} | Nome: {_nome} | Preço: {_preco}";
    //}
}