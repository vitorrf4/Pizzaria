using System.Collections.Generic;
using JetBrains.Annotations;
using Pizzaria.Models;
using Xunit;

namespace Pizzaria.Tests.Models;

[TestSubject(typeof(PizzaPedido))]
public class PizzaPedidoTest
{
    [Fact(DisplayName = "Cria Pizza Pedido com o Construtor Completo")]
    public void when_create_pizza_pedido_then_success()
    {
        var sabores = new List<Sabor> { new ("Sabor1", 10), new ("Sabor2", 15) };
        var tamanho = new Tamanho("Tamanho1", 8, 3, 1.5);
        const int quantidade = 2;

        var pizzaPedido = new PizzaPedido(sabores, tamanho, quantidade);

        Assert.NotNull(pizzaPedido);
        Assert.Equal(sabores, pizzaPedido.Sabores);
        Assert.Equal(tamanho, pizzaPedido.Tamanho);
        Assert.Equal(quantidade, pizzaPedido.Quantidade);
    }

    [Fact(DisplayName = "Calcula Preço da PizzaPedido")]
    public void when_calculate_preco_then_correct_price()
    {
        var sabores = new List<Sabor> { new("Sabor1", 10), new("Sabor2", 15) };
        var tamanho = new Tamanho("Tamanho1", 8, 3, 1.5);
        const int quantidade = 2;
        const double expectedPreco = (15d + 10d) / 2 * 1.5 * 2;

        var pizzaPedido = new PizzaPedido(sabores, tamanho, quantidade);

        Assert.Equal(expectedPreco, pizzaPedido.Preco);
    }
}