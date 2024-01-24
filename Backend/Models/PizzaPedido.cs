namespace pizzaria;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class PizzaPedido 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int  Id { get; set; }
    public List<Sabor> Sabores { get; set; } = new List<Sabor>();
    public Tamanho Tamanho { get; set; } = new Tamanho();
    public double Preco { get; set; }
    public int Quantidade { get; set;}
    [JsonIgnore]
    public PedidoFinal? PedidoFinal { get; set; }

    public PizzaPedido() { }

    public PizzaPedido(List<Sabor> sabores, Tamanho tamanho, int quantidade = 1)
    {
        Sabores = sabores;
        Tamanho = tamanho;
        Quantidade = quantidade;
        CalcularPreco();
    }

    public double CalcularPreco()
    {
        // Fórmula do preco = ((sabor1 + sabor2...saborN) / quantidade de sabores) + (preco * (preco do tamanho / 100))
        double precoTotal = 0.0;

        Sabores.ForEach(sabor => precoTotal += sabor.Preco);

        if (Sabores.Count > 1)
            precoTotal /= Sabores.Count;

        precoTotal *= Tamanho.MultiplicadorPreco;
        Preco = precoTotal * Quantidade;

        return Preco;
    }

    override public string ToString()
    {
        Console.Write($"Sabores: ");
        for (int i = 0; i < Sabores.Count; i++)
        {
            Console.Write(Sabores[i].Nome);
            if (i != (Sabores.Count - 1))
                Console.Write(", ");
        }

        Console.Write($" | Tamanho: {Tamanho.Nome} | ");
        Console.Write($"Preço: R${Preco}");

        return "";
    }
}