using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzaria;

public class PedidoFinal 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Cliente Cliente { get; set; } = new Cliente();
    public double PrecoTotal { get; private set; }
    public DateTime HoraPedido { get; set; }
    public List<PizzaPedido> Pizzas { get; set; }= new List<PizzaPedido>();
    public List<AcompanhamentoPedido> Acompanhamentos { get; set; } = new List<AcompanhamentoPedido>();

    public PedidoFinal() { }

    public PedidoFinal(Cliente cliente, List<PizzaPedido> pizzas, List<AcompanhamentoPedido> ?acompanhamentos)
    {
        Cliente = cliente;
        Pizzas = pizzas;
        Acompanhamentos = acompanhamentos ?? new List<AcompanhamentoPedido>();
        HoraPedido = DateTime.Now;
        CalcularPrecoTotal();
    }

    public void CalcularPrecoTotal()
    {
        double precoTotal = 0;

        Pizzas.ForEach(pizza => precoTotal += pizza.Preco);
        Acompanhamentos?.ForEach(acomp =>  precoTotal += acomp.Preco);

        PrecoTotal = precoTotal;
    }

    public override string ToString()
    {
        int index = 1;

        Console.WriteLine($"Pedido #{Id}");
        Console.WriteLine($"Cliente: {Cliente.Nome}");
        Console.Write($"PIZZAS \n");
        Pizzas.ForEach(pizza => 
        {
            Console.WriteLine($"Pizza #{index}: ");
            Console.WriteLine(pizza);
            index++;
        });
        Acompanhamentos?.ForEach(Console.WriteLine);
        Console.Write(Cliente.Endereco.Regiao + " \n");
        Console.Write($"Hora do Pedido: {HoraPedido} | ");
        Console.Write($"Preço Total do Pedido: R${PrecoTotal}");

        return "";
    }
}