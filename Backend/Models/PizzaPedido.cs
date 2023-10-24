namespace pizzaria;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class PizzaPedido 
{
    [Key]
    public int  Id { get; set; }
    public List<Sabor> Sabores { get; set; } = new();
    public Tamanho Tamanho { get; set; }
    public double Preco { get; private set; }
    [JsonIgnore]
    public PedidoFinal? PedidoFinal { get; set; }

    public PizzaPedido() { }

    public PizzaPedido(List<Sabor> sabores, Tamanho tamanho)
    {
        Sabores = sabores;
        Tamanho = tamanho;
        CalcularPreco();
    }

    public double CalcularPreco()
    {
        // preco = ((sabor1 + sabor2...saborN) / quantidade de sabores) + (preco * tamanho)

        double precoTotal = 0.0;

        foreach(Sabor sabor in Sabores)
        {
            precoTotal += sabor.Preco;
        }

        if (Sabores.Count > 0)
        {
            precoTotal = precoTotal / Sabores.Count;
        }

        precoTotal += precoTotal * (Tamanho.MultiplicadorPreco / 100.0);

        Preco = precoTotal;
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