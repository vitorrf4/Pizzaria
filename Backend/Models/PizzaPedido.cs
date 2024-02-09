using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzaria.Models;

public class PizzaPedido 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int  Id { get; set; }
    // TODO: mudar List pra HashSet
    public List<Sabor> Sabores { get; set; } = new();
    public Tamanho Tamanho { get; set; } = new();
    public double Preco { get; set; }
    public int Quantidade { get; set;}

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
        // Fórmula do preco = ((sabor1 + sabor2...saborN) / quantidade de sabores) + (preco * preco do tamanho) * quantidade
        var precoTotal = Sabores.Sum(sabor => sabor.Preco);

        if (Sabores.Count > 1)
            precoTotal /= Sabores.Count;

        precoTotal *= Tamanho.MultiplicadorPreco;
        Preco = precoTotal * Quantidade;

        return Preco;
    }

    public override string ToString()
    {
        var sabores = "Sabores: ";
        for (var i = 0; i < Sabores.Count; i++)
        {
            sabores += Sabores[i].Nome;
            if (i != (Sabores.Count - 1))
                sabores += ", ";
        }

        sabores += $" | Tamanho: {Tamanho.Nome} | ";
        sabores += $"Preço: R${Preco}";

        return sabores;
    }
}