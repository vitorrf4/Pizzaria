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
    [Required(ErrorMessage = "O ClienteId é obrigatório")]
    public int ClienteId { get; set; }
    [Required(ErrorMessage = "O endereço é obrigatório")]
    public Endereco Endereco { get; set; }
    public double PrecoTotal { get; private set; }
    public DateTime HoraPedido { get; init; }
    [MinLength(1, ErrorMessage = "Pelo menos uma pizza é obrigatória")]
    public List<PizzaPedido> Pizzas { get; set; } = new();
    public List<AcompanhamentoPedido> Acompanhamentos { get; set; } = new();

    private PedidoFinal() { }

    public PedidoFinal(int clienteId, Endereco endereco, List<PizzaPedido> pizzas)
    {
        ClienteId = clienteId;
        Endereco = endereco;
        Pizzas = pizzas;
        HoraPedido = DateTime.Now;
        CalcularPrecoTotal();
    }
    
    public PedidoFinal(int clienteId, Endereco endereco, List<PizzaPedido> pizzas,
        List<AcompanhamentoPedido> acompanhamentos) : this(clienteId, endereco, pizzas)
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