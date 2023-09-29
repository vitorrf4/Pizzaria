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
            .Include("Cliente.Endereco")
            .Include("Acompanhamentos.Acompanhamento")
            .Include("Pizzas.Tamanho")
            .Include("Pizzas.Sabores")
            .Include("Promocao")
            .ToListAsync();

        return PedidoFinal;
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<PedidoFinal>> Buscar([FromRoute] int id)   
    {
        var PedidoFinal = await _context.PedidoFinal
            .Where(pedidoFinal => pedidoFinal.Id == id)
            .Include("Cliente.Endereco")
            .Include("Acompanhamentos.Acompanhamento")
            .Include("Pizzas.Tamanho")
            .Include("Pizzas.Sabores")
            .Include("Promocao")
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
        if (pedidoFinal.Acompanhamentos != null)
        {
            var acompanhamentosCompletos = new List<AcompanhamentoPedido>();
            foreach (AcompanhamentoPedido acompanhamento in pedidoFinal.Acompanhamentos)
            {
                var acompanhamentoCompleto = await _context.AcompanhamentoPedido
                    .Where(acomp => acomp.Id == acompanhamento.Id)
                    .Include("PedidoFinal")
                    .FirstOrDefaultAsync();

                if (acompanhamentoCompleto == null) return BadRequest("O acompanhamento pedido não foi encontrado");
                if (acompanhamentoCompleto.PedidoFinal != null)
                    return BadRequest($"O acompanhamento {acompanhamentoCompleto.Id} já está associada à um pedido final");

                acompanhamentosCompletos.Add(acompanhamentoCompleto);
            }
            pedidoFinal.Acompanhamentos = acompanhamentosCompletos;
        }


        //Pizzas

        var pizzasCompletas = new List<PizzaPedido>();

        foreach (PizzaPedido pizza in pedidoFinal.Pizzas)
        {
            var pizzaCompleta = await _context.PizzaPedido
                .Where(p => p.Id == pizza.Id)
                .Include("PedidoFinal")
                .FirstOrDefaultAsync();

            if (pizzaCompleta == null) return BadRequest("O Pedido pizza não foi encontrado");
            if (pizzaCompleta.PedidoFinal != null) 
                return BadRequest($"A pizza {pizzaCompleta.Id} já está associada à um pedido final");

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
        var pedidoNoBanco = await _context.PedidoFinal.FindAsync(pedidoFinal.Id);
        if (pedidoNoBanco == null) return NotFound("Pedido não encontrado");

        var clienteCompleto = await _context.Cliente
           .Where(clienteNoBanco => clienteNoBanco.Cpf == pedidoFinal.Cliente.Cpf)
           .Include("Endereco.Regiao")
           .FirstOrDefaultAsync();

        if (clienteCompleto == null) return BadRequest("O cliente não foi encontrado");

        pedidoFinal.Cliente = clienteCompleto;

        //Acompanhamentos
        var acompanhamentosCompletos = new List<AcompanhamentoPedido>();

        foreach (AcompanhamentoPedido acompanhamento in pedidoFinal.Acompanhamentos)
        {
            var acompanhamentoCompleto = await _context.AcompanhamentoPedido
                .Where(acomp => acomp.Id == acompanhamento.Id)
                .Include("PedidoFinal")
                .FirstOrDefaultAsync();

            if (acompanhamentoCompleto == null) return BadRequest("O acompanhamento pedido não foi encontrado");
            if (acompanhamentoCompleto.PedidoFinal != null && acompanhamentoCompleto.PedidoFinal.Id != pedidoNoBanco.Id)
                return BadRequest($"O acompanhamento {acompanhamentoCompleto.Id} já está associada à um pedido final");

            acompanhamentosCompletos.Add(acompanhamentoCompleto);
        }

        pedidoFinal.Acompanhamentos = acompanhamentosCompletos;

        //Pizzas

        var pizzasCompletas = new List<PizzaPedido>();

        foreach (PizzaPedido pizza in pedidoFinal.Pizzas)
        {
            var pizzaCompleta = await _context.PizzaPedido
                .Where(p => p.Id == pizza.Id)
                .Include("PedidoFinal")
                .FirstOrDefaultAsync();

            if (pizzaCompleta == null) return BadRequest("O Pedido pizza não foi encontrado");
            if (pizzaCompleta.PedidoFinal != null && pizzaCompleta.PedidoFinal.Id != pedidoNoBanco.Id)
                return BadRequest($"A pizza {pizzaCompleta.Id} já está associada à um pedido final");

            pizzasCompletas.Add(pizzaCompleta);
        }
        pedidoFinal.Pizzas = pizzasCompletas;


        // sem essa linha o banco de dados da erro por ter duas variaveis com o mesmo ID no programa
        _context.Entry(pedidoNoBanco).State = EntityState.Detached;

        pedidoFinal.CalcularPrecoTotal();
        _context.PedidoFinal.Update(pedidoFinal);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir")]
    public async Task<IActionResult> Excluir(int id)
    {
        var pedidoFinal = await _context.PedidoFinal
            .Where(pedido => pedido.Id == id)
            .Include("Acompanhamentos")
            .Include("Pizzas")
            .FirstOrDefaultAsync();


        if (pedidoFinal is null) return NotFound("Pedido não encontrado");
        
        foreach (AcompanhamentoPedido acompanhamento in pedidoFinal.Acompanhamentos)
        {
            _context.Remove(acompanhamento);
        }
        foreach (PizzaPedido pizza in pedidoFinal.Pizzas)
        {
            _context.Remove(pizza);
        }

        _context.PedidoFinal.Remove(pedidoFinal);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
