using System.ComponentModel.DataAnnotations;

namespace pizzaria;

public class PedidoFinal 
{
    [Key]
    public int Id { get; set; }
    public Cliente Cliente { get; set; }
    public List<PizzaPedido> Pizzas { get; set; }
    public List<AcompanhamentoPedido> ?Acompanhamentos { get; set; }
    public double PrecoTotal { get; private set; }
    public DateTime HoraPedido { get; set; }
    public Promocao ?Promocao { get; private set; }

    public PedidoFinal(){ }

    public PedidoFinal(Cliente cliente, List<PizzaPedido> pizzas, List<AcompanhamentoPedido> acompanhamentos){
        Cliente = cliente;
        Pizzas = pizzas;
        Acompanhamentos = acompanhamentos;
        HoraPedido = DateTime.Now;
        CalcularPrecoTotal();
    }

    public double CalcularPrecoTotal()
    {
        double precoPedido = 0.0;
        double precoAcompanhamento = 0.0;
        double precoRegiao = Cliente.Endereco.Regiao.Preco;
        int aniversarioCliente = Cliente.DataAniversario.DayOfYear;
        int dataPedido = DateOnly.FromDateTime(HoraPedido).DayOfYear;

        foreach (PizzaPedido pizza in Pizzas)
        {
            precoPedido += pizza.Preco;
        }

        if (Acompanhamentos != null)
        {
            foreach (AcompanhamentoPedido acompanhamento in Acompanhamentos)
            {
                precoAcompanhamento += acompanhamento.PrecoTotal;
            }
        }

        PrecoTotal = precoPedido + precoAcompanhamento + precoRegiao;

        if (aniversarioCliente == dataPedido)
        {
            Promocao = new Promocao(Id, 10);
            PrecoTotal = PrecoTotal - (PrecoTotal * (Promocao.Desconto / 100));
        }

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
        Acompanhamentos.ForEach(Console.WriteLine);
        Console.Write(Cliente.Endereco.Regiao + " \n");
        Console.Write($"Hora do Pedido: {HoraPedido} | ");
        Console.Write($"Preço Total do Pedido: R${PrecoTotal}");

        return "";
    }
}