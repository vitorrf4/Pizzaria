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
    public List<Sabor> Sabores { get; set; }
    public Tamanho Tamanho { get; set; }
    public double Preco { get; set; }
    public int Quantidade { get; set;}
    [JsonIgnore]
    public PedidoFinal? PedidoFinal { get; set; }

    public PizzaPedido() 
    {
        Sabores = new List<Sabor>();
        Tamanho = new Tamanho();
    }

    public PizzaPedido(List<Sabor> sabores, Tamanho tamanho, int quantidade)
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

        foreach (Sabor sabor in Sabores)
        {
            precoTotal += sabor.Preco;
        }

        if (Sabores.Count > 1)
        {
            precoTotal = precoTotal / Sabores.Count;
        }

        precoTotal += precoTotal * (Tamanho.MultiplicadorPreco / 100.0);

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