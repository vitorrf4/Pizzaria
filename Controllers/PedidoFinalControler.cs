using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("[controller]")]
public class PedidoFinalController : ControllerBase
{
    private readonly ILogger<PedidoFinalController> _logger;
    private PizzariaDBContext _context;

    public PedidoFinalController(PizzariaDBContext context, ILogger<PedidoFinalController> logger) 
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<PedidoFinal>>> Listar()
    {
        var PedidoFinal = await _context.PedidoFinal
            .Include("Cliente")
            .Include("Acompanhamentos.Acompanhamento")
            .Include("Pizzas.Tamanho")
            .Include("Pizzas.Sabores")
            .ToListAsync();

        return PedidoFinal;
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<PedidoFinal>> Buscar([FromRoute] int id)   
    {
        var PedidoFinal = await _context.PedidoFinal
            .Where(pedidoFinal => pedidoFinal.Id == id)
            .Include("Cliente")
            .Include("Acompanhamentos.Acompanhamento")
            .Include("Pizzas.Tamanho")
            .Include("Pizzas.Sabores")
            .FirstOrDefaultAsync();
            
        if (PedidoFinal == null)
            return NotFound();
        
        return Ok(PedidoFinal);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(PedidoFinal pedidoFinal)
    {
        //Cliente
        //Include() é necessario para o calculo final do pedido que leva em conta o preço da regiao
        var clienteCompleto = await _context.Cliente
            .Where(clienteNoBanco => clienteNoBanco.Cpf == pedidoFinal.Cliente.Cpf)
            .Include("Endereco.Regiao")
            .FirstOrDefaultAsync();

        if (clienteCompleto == null) return BadRequest("O cliente não foi encontrado");

        pedidoFinal.Cliente = clienteCompleto;

        //Acompanhamentos
        var acompanhamentosCompletos = new List<AcompanhamentoPedido>();

        foreach (AcompanhamentoPedido pedido in pedidoFinal.Acompanhamentos)
        {
            var pedidoCompleto = await _context.AcompanhamentoPedido.FindAsync(pedido.Id);
            if (pedidoCompleto == null) return BadRequest("O acompanhamento pedido não foi encontrado");

            acompanhamentosCompletos.Add(pedidoCompleto);
        }

        pedidoFinal.Acompanhamentos = acompanhamentosCompletos;

        //Pizzas
        var pizzasCompletas = new List<PizzaPedido>();

        foreach (PizzaPedido pizza in pedidoFinal.Pizzas)
        {
            var pizzaCompleta = await _context.PizzaPedido.FindAsync(pizza.Id);
            if (pizzaCompleta == null) return BadRequest("O Pedido Pizza não foi encontrado");

            pizzasCompletas.Add(pizzaCompleta);
        }
        pedidoFinal.Pizzas = pizzasCompletas;

        
        pedidoFinal.CalcularPrecoTotal();

        await _context.AddAsync(pedidoFinal);
        await _context.SaveChangesAsync();
        return Created("", pedidoFinal);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar (PedidoFinal pedidoFinal)
    {
        _context.PedidoFinal.Update(pedidoFinal);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir")]
    public async Task<IActionResult> Excluir(int id)
    {
        var pedidoFinal = await _context.PedidoFinal.FindAsync(id);
        if(pedidoFinal is null) return NotFound();
        
        _context.PedidoFinal.Remove(pedidoFinal);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
