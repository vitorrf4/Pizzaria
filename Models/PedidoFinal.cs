using System.ComponentModel.DataAnnotations;

namespace pizzaria;

public class PedidoFinal 
{
    [Key]
    public int Id { get; set; }
    public Cliente Cliente { get; set; }
    public List<PizzaPedido> Pizzas { get; set; }
    public List<AcompanhamentoPedido> Acompanhamentos { get; set; }
    public DateTime HoraPedido { get; set; } = DateTime.Now;
    public Regiao Regiao { get; set; }
    public double PrecoTotal { get; private set; }

    public PedidoFinal(){ }

    public PedidoFinal(int id, Cliente cliente, List<PizzaPedido> pizzas, List<AcompanhamentoPedido> acompanhamentos, Regiao regiao)
    {
        Id = id;
        Cliente = cliente;
        Pizzas = pizzas;
        Acompanhamentos = acompanhamentos;
        Regiao = regiao;
        CalcularPrecoTotal();
    }

    private double CalcularPrecoTotal(){
        double precoPedido = 0.0;
        double precoAcompanhamento = 0.0;
        double precoRegiao = Regiao.Preco;

        foreach(PizzaPedido pizza in Pizzas)
        {
            precoPedido += pizza.Preco;
        }

        foreach(AcompanhamentoPedido acompanhamento in Acompanhamentos)
        {
            precoAcompanhamento += acompanhamento.PrecoTotal;
        }

        PrecoTotal = precoPedido + precoAcompanhamento + precoRegiao;
        return PrecoTotal;
    }

    public override string ToString()
    {
        int index = 1;

        Console.WriteLine($"Pedido #{Id}");
        Console.WriteLine($"Cliente: {Cliente.Nome}");
        Console.Write($"PIZZAS \n");
        Pizzas.ForEach(delegate(PizzaPedido pizza)
        {
            Console.WriteLine($"Pizza #{index}: ");
            Console.WriteLine(pizza);
            index++;
        });
        Acompanhamentos.ForEach(Console.WriteLine);
        Console.Write(Regiao + " \n");
        Console.Write($"Hora do Pedido: {HoraPedido} | ");
        Console.Write($"Preço Total do Pedido: R${PrecoTotal}");

        return "";
    }
}