using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzaria;

public class PedidoFinal 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Cliente Cliente { get; set; }
    public List<PizzaPedido> ?Pizzas { get; set; }
    public List<AcompanhamentoPedido> ?Acompanhamentos { get; set; }
    public double PrecoTotal { get; private set; }
    public DateTime HoraPedido { get; set; }

    public PedidoFinal() 
    {
        Cliente = new Cliente();
        Pizzas = new List<PizzaPedido>();
    }

    public PedidoFinal(Cliente cliente, List<PizzaPedido> pizzas, List<AcompanhamentoPedido> ?acompanhamentos)
    {
        Cliente = cliente;
        Pizzas = pizzas;
        Acompanhamentos = acompanhamentos;
        HoraPedido = DateTime.Now;
        CalcularPrecoTotal();
    }

    public double CalcularPrecoTotal()
    {
        double precoTotal = 0;

        Pizzas.ForEach(pizza => precoTotal += pizza.Preco);
        
        Acompanhamentos?.ForEach(acomp =>  precoTotal += acomp.Preco);
            
        int aniversarioCliente = Cliente.DataAniversario.DayOfYear;
        int dataPedido = DateOnly.FromDateTime(HoraPedido).DayOfYear;

        PrecoTotal = precoTotal;
        return PrecoTotal;
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