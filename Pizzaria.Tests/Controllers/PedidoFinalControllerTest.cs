using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pizzaria.Controllers;
using Pizzaria.Models;
using Pizzaria.Services;
using Xunit;

namespace Pizzaria.Tests.Controllers;

[TestSubject(typeof(PedidoFinalController))]
public class PedidoFinalControllerTest
{
    [Fact(DisplayName = "Listar - Ok")]
    public async Task when_listar_is_successful_then_return_list_of_PedidoFinal_objects_()
    {
        var pedidos = new List<PedidoFinal> { CreatePedidoFinal(), CreatePedidoFinal() };
        var serviceMock = new Mock<IPedidoFinalService>();
        serviceMock.Setup(s => s.Listar()).ReturnsAsync(pedidos);
        var controller = new PedidoFinalController(serviceMock.Object);

        var result = await controller.Listar();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedPedidoFinals = Assert.IsAssignableFrom<IEnumerable<PedidoFinal>>(okResult.Value);
        Assert.Equal(pedidos, returnedPedidoFinals);
    }

    [Fact(DisplayName = "Buscar Por Id - Ok")]
    public async Task when_buscar_por_id_success_then_return_pedido() 
    {
        var pedidoFinal = CreatePedidoFinal();
        var serviceMock = new Mock<IPedidoFinalService>();
        serviceMock.Setup(s => s.BuscarPorId(It.IsAny<int>())).ReturnsAsync(pedidoFinal);
        var controller = new PedidoFinalController(serviceMock.Object);
        const int id = 1;

        var result = await controller.Buscar(id);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedPedidoFinal = Assert.IsAssignableFrom<PedidoFinal>(okResult.Value);
        Assert.Equal(pedidoFinal, returnedPedidoFinal);
    }

    [Fact(DisplayName = "Buscar Por Id - Not Found")]
    public async Task when_no_entity_on_database_then_return_not_found()  
    {
        var serviceMock = new Mock<IPedidoFinalService>();
        serviceMock.Setup(s => s.BuscarPorId(It.IsAny<int>())).ReturnsAsync((PedidoFinal)null);
        var controller = new PedidoFinalController(serviceMock.Object);
        const int id = 1;

        var result = await controller.Buscar(id);

        Assert.IsType<NotFoundResult>(result.Result);
    }
    
    [Fact(DisplayName = "Cadastrar - Created")]
    public async Task when_cadastrar_success_then_return_created_pedido()
    {
        var pedidoFinal = CreatePedidoFinal();
        var serviceMock = new Mock<IPedidoFinalService>();
        serviceMock.Setup(s => s.Cadastrar(It.IsAny<PedidoFinal>())).ReturnsAsync(true);
        var controller = new PedidoFinalController(serviceMock.Object);

        var result = await controller.Cadastrar(pedidoFinal);

        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal(pedidoFinal, createdResult.Value);
    }

    [Fact(DisplayName = "Cadastrar - Bad Request")]
    public async Task when_invalid_pedido_then_bad_request()
    {
        var pedidoFinal = CreatePedidoFinal();
        var serviceMock = new Mock<IPedidoFinalService>();
        serviceMock.Setup(s => s.Cadastrar(It.IsAny<PedidoFinal>())).ReturnsAsync(false);
        var controller = new PedidoFinalController(serviceMock.Object);

        var result = await controller.Cadastrar(pedidoFinal);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact(DisplayName = "Excluir - No Content")]
    public async Task when_excluir_given_valid_entity_then_no_content()
    {
        var serviceMock = new Mock<IPedidoFinalService>();
        serviceMock.Setup(s => s.Excluir(It.IsAny<int>())).ReturnsAsync(true);
        var controller = new PedidoFinalController(serviceMock.Object);
        const int id = 1;
        
        var result = await controller.Excluir(id);
        
        Assert.IsType<NoContentResult>(result);
    }

    private static PedidoFinal CreatePedidoFinal()
    {
        return new PedidoFinal(1, null, new List<PizzaPedido>());
    }

}