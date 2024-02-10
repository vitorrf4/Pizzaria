using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Pizzaria.Models;

[PublicAPI]
public class PedidoFinal 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required(ErrorMessage = "O Cliente é obrigatório")]
    public Cliente Cliente { get; set; }
    [MinLength(1, ErrorMessage = "Pelo menos uma pizza é obrigatória")]
    public List<PizzaPedido> Pizzas { get; set; } = new();
    public List<AcompanhamentoPedido> Acompanhamentos { get; set; } = new();
    public double PrecoTotal { get; private set; }
    public DateTime HoraPedido { get; init; }

    private PedidoFinal() { }

    public PedidoFinal(Cliente cliente, List<PizzaPedido> pizzas)
    {
        Cliente = cliente;
        Pizzas = pizzas;
        HoraPedido = DateTime.Now;
        CalcularPrecoTotal();
    }
     
    public PedidoFinal(Cliente cliente, List<PizzaPedido> pizzas,
        List<AcompanhamentoPedido> acompanhamentos) : this(cliente, pizzas)
    {
        Acompanhamentos = acompanhamentos;
        CalcularPrecoTotal();
    }
    
    public void CalcularPrecoTotal()
    {
        double precoTotal = 0;

        Pizzas.ForEach(pizza => precoTotal += pizza.Preco);
        Acompanhamentos.ForEach(acomp =>  precoTotal += acomp.Preco);

        PrecoTotal = precoTotal;
    }

    public override string ToString()
    {
        var index = 1;
        
        Console.WriteLine($"Pedido #{Id}");
        Console.WriteLine($"Cliente: {Cliente}");
        Console.Write($"PIZZAS \n");
        Pizzas.ForEach(pizza => 
        {
            Console.WriteLine($"Pizza #{index}: ");
            Console.WriteLine(pizza);
            index++;
        });
        Acompanhamentos.ForEach(Console.WriteLine);
        Console.Write($"Hora do Pedido: {HoraPedido} | ");
        Console.Write($"Preço Total do Pedido: R${PrecoTotal}");

        return "";
    }
}