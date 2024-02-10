using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Pizzaria.Models;

[PublicAPI]
public class PizzaPedido 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int  Id { get; set; }
    // TODO: mudar List pra HashSet
    [MinLength(1, ErrorMessage = "A pizza deve ter no mínimo um sabor")]
    public List<Sabor> Sabores { get; set; } = new();
    [Required(ErrorMessage = "O tamanho é obrigatório")]
    public Tamanho Tamanho { get; set; }
    public double Preco { get; private set; }
    [Range(1, 100, ErrorMessage = "A quantidade deve estar entre 1 e 100")]
    public int Quantidade { get; set; }

    private PizzaPedido() { }

    public PizzaPedido(List<Sabor> sabores, Tamanho tamanho, int quantidade = 1)
    {
        Sabores = sabores;
        Tamanho = tamanho;
        Quantidade = quantidade;
        CalcularPreco();
    }

    public void CalcularPreco()
    {
        // Fórmula do preco = (sabor1 + sabor2...saborN) / quantidade de sabores +
        //                    (preco * preco do tamanho) * quantidade
        var precoTotal = Sabores.Sum(sabor => sabor.Preco);

        if (Sabores.Count > 1)
            precoTotal /= Sabores.Count;

        precoTotal *= Tamanho.MultiplicadorPreco;
        Preco = precoTotal * Quantidade;
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