using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Testing;
using Pizzaria.DTOs;
using Pizzaria.Models;
using Pizzaria.Services;
using Xunit;

namespace Pizzaria.Tests.Services;

[TestSubject(typeof(PedidoFinalService))]
public class PedidoFinalServiceTest : IClassFixture<WebApplicationFactory<PedidoFinalService>>
{
    private readonly WebApplicationFactory<PedidoFinalService> _factory;
    private readonly HttpClient _client;

    public PedidoFinalServiceTest(WebApplicationFactory<PedidoFinalService> factory)
    {
        _factory = factory;
        _factory.Services.GetService(typeof(PedidoFinalService));
        _client = _factory.CreateDefaultClient();
    }

    [Fact]
    public async Task when_get_pedidos_then_returns_list()
    {
        _factory.Services.GetService(typeof(PedidoFinalService));
        var c = _factory.CreateDefaultClient();
        var res = await c.GetAsync("/pedido-final/1");
    
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
    }
    
    [Fact]
    public async Task when_fazer_pedido_then_success()
    {
        const string expectedSabor = "brocolis com bacon";
        var pizzasDto = new PizzaPedidoDto(new List<string> { expectedSabor }, "grande", 1);
        var pedidoDto = new PedidoFinalDto(1, new List<PizzaPedidoDto>{pizzasDto});
        var request = JsonContent.Create(pedidoDto);
        
        var response = await _client.PostAsync("/pedido-final", request);
        var pedido = await response.Content.ReadFromJsonAsync<PedidoFinal>();
        var actualSabor = pedido.Pizzas[0].Sabores[0].Nome;
        
        var saboresSaoIguais = expectedSabor.Equals(actualSabor, StringComparison.CurrentCultureIgnoreCase);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Single(pedidoDto.Pizzas);
        Assert.True(saboresSaoIguais);
    }

    [Fact]
    public async Task when_fazer_pedido_with_multiple_sabores_then_success()
    {
        List<PizzaPedidoDto> pizzasDto = new()
        {
            new PizzaPedidoDto(new List<string> { "frango" }, "pequena", 1),
            new PizzaPedidoDto(new List<string> { "frango", "calabresa" }, "grande", 1)
        };
        var pedidoDto = new PedidoFinalDto(1, pizzasDto);
        var request = JsonContent.Create(pedidoDto);
        
        var res = await _client.PostAsync("/pedido-final", request);
        var pizza = await res.Content.ReadFromJsonAsync<PedidoFinal>();

        var saborCount1 = pizza.Pizzas[0].Sabores.Count;
        var saborCount2 = pizza.Pizzas[1].Sabores.Count;
        
        Assert.Equal(HttpStatusCode.Created, res.StatusCode);
        Assert.Equal(1, saborCount1);
        Assert.Equal(2, saborCount2);
    }
}