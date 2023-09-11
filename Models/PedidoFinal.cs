namespace pizzaria;

public class PedidoFinal 
{
    private int _id;
    private Cliente _cliente;
    private List<PizzaPedido> _pizzas = new();
    private List<AcompanhamentoPedido> _acompanhamentos;
    private double _precoTotal;
    private DateTime _horaPedido;
    private Regiao _regiao;

    public PedidoFinal(){}

    public PedidoFinal(int id, Cliente cliente, List<PizzaPedido> pizzas, List<AcompanhamentoPedido> acompanhamentos, Regiao regiao){
        _id = id;
        _cliente = cliente;
        _pizzas = pizzas;
        _acompanhamentos = acompanhamentos;
        _horaPedido = DateTime.Now;
        _regiao = regiao;
        CalcularPrecoTotal();
    }

    private double CalcularPrecoTotal(){
        double precoPedido = 0.0;
        double precoAcompanhamento = 0.0;
        double precoRegiao = _regiao.Preco;

        foreach(PizzaPedido pizza in _pizzas)
        {
            precoPedido += pizza.Preco;
        }

        foreach(AcompanhamentoPedido acompanhamento in _acompanhamentos)
        {
            precoAcompanhamento += acompanhamento.PrecoTotal;
        }

        _precoTotal = precoPedido + precoAcompanhamento + precoRegiao;
        return _precoTotal;
    }

    public override string ToString()
    {
        int index = 1;

        Console.WriteLine($"Pedido #{_id}");
        Console.WriteLine($"Cliente: {_cliente.Nome}");
        Console.Write($"PIZZAS \n");
        _pizzas.ForEach(delegate(PizzaPedido pizza)
        {
            Console.WriteLine($"Pizza #{index}: ");
            Console.WriteLine(pizza);
            index++;
        });
        _acompanhamentos.ForEach(Console.WriteLine);
        Console.Write(_regiao + " \n");
        Console.Write($"Hora do Pedido: {_horaPedido} | ");
        Console.Write($"Preço Total do Pedido: R${_precoTotal}");

        return "";
    }
}