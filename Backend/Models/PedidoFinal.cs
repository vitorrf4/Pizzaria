using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzaria.Models;

public class PedidoFinal 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Endereco Endereco { get; set; } = new();
    public double PrecoTotal { get; private set; }
    public DateTime HoraPedido { get; set; }
    public List<PizzaPedido> Pizzas { get; set; } = new();
    public List<AcompanhamentoPedido> Acompanhamentos { get; set; } = new();

    public PedidoFinal() { }

    public PedidoFinal(int clienteId, Endereco endereco, 
                       List<PizzaPedido> pizzas, List<AcompanhamentoPedido> ?acompanhamentos)
    {
        ClienteId = clienteId;
        Endereco = endereco;
        Pizzas = pizzas;
        Acompanhamentos = acompanhamentos ?? new List<AcompanhamentoPedido>();
        HoraPedido = DateTime.Now;
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
        Console.WriteLine($"Cliente: {ClienteId}");
        Console.Write($"PIZZAS \n");
        Pizzas.ForEach(pizza => 
        {
            Console.WriteLine($"Pizza #{index}: ");
            Console.WriteLine(pizza);
            index++;
        });
        Acompanhamentos.ForEach(Console.WriteLine);
        Console.Write($"Endereço de envio: {Endereco.Rua}" + " \n");
        Console.Write($"Hora do Pedido: {HoraPedido} | ");
        Console.Write($"Preço Total do Pedido: R${PrecoTotal}");

        return "";
    }
}