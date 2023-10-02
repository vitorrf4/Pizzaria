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

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<PedidoFinal>>> Listar()
    {
        var pedidoFinal = await GetPedidosFinaisComTodasAsPropriedades().ToListAsync();
        return pedidoFinal;
    }

    [HttpGet]
    [Route("listar/{id}")]
    public async Task<ActionResult<PedidoFinal>> Buscar([FromRoute] int id)
    {
        var pedidoFinal = await GetPedidoFinalComTodasAsPropriedades(id).FirstOrDefaultAsync();

        if (pedidoFinal == null)
            return NotFound("Pedido não encontrado");

        return Ok(pedidoFinal);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(PedidoFinal pedidoFinal)
    {
        var clienteCompleto = await GetClienteComTodasAsPropriedades(pedidoFinal.Cliente.Cpf);
        if (clienteCompleto == null) return BadRequest("O cliente não foi encontrado");

        pedidoFinal.Cliente = clienteCompleto;

        if (pedidoFinal.Acompanhamentos != null)
        {
            var acompanhamentosCompletos = await GetAcompanhamentosCompletos(pedidoFinal.Acompanhamentos);
            if (acompanhamentosCompletos == null) return BadRequest("O acompanhamento pedido não foi encontrado");
            pedidoFinal.Acompanhamentos = acompanhamentosCompletos;
        }

        var pizzasCompletas = await GetPizzasCompletas(pedidoFinal.Pizzas);
        if (pizzasCompletas == null) return BadRequest("Pizza pedido inválido");
        pedidoFinal.Pizzas = pizzasCompletas;

        pedidoFinal.CalcularPrecoTotal();

        await _context.AddAsync(pedidoFinal);
        await _context.SaveChangesAsync();
        return Created("", pedidoFinal);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar(PedidoFinal pedidoFinal)
    {
        var pedidoNoBanco = await _context.PedidoFinal.FindAsync(pedidoFinal.Id);
        if (pedidoNoBanco == null) return NotFound("Pedido não encontrado");

        var clienteCompleto = await GetClienteComTodasAsPropriedades(pedidoFinal.Cliente.Cpf);
        if (clienteCompleto == null) return BadRequest("O cliente não foi encontrado");

        pedidoFinal.Cliente = clienteCompleto;

        if (pedidoFinal.Acompanhamentos != null)
        {
            var acompanhamentosCompletos = await GetAcompanhamentosCompletos(pedidoFinal.Acompanhamentos);
            if (acompanhamentosCompletos == null) return BadRequest("O acompanhamento pedido não foi encontrado");
            pedidoFinal.Acompanhamentos = acompanhamentosCompletos;
        }

        var pizzasCompletas = await GetPizzasCompletas(pedidoFinal.Pizzas);
        if (pizzasCompletas == null) return BadRequest("Pizza pedido inválido");
        pedidoFinal.Pizzas = pizzasCompletas;

        // linha necessária para o _context não dar erro de conflito
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
        var pedidoFinal = await GetPedidoFinalComTodasAsPropriedades(id).FirstOrDefaultAsync();

        if (pedidoFinal == null) return NotFound("Pedido não encontrado");

        if (pedidoFinal.Acompanhamentos != null)
        {
            foreach (AcompanhamentoPedido acompanhamento in pedidoFinal.Acompanhamentos)
            {
                _context.Remove(acompanhamento);
            }
        }

        foreach (PizzaPedido pizza in pedidoFinal.Pizzas)
        {
            _context.Remove(pizza);
        }

        _context.PedidoFinal.Remove(pedidoFinal);
        await _context.SaveChangesAsync();
        return Ok();
    }

    //

    private IQueryable<PedidoFinal> GetPedidosFinaisComTodasAsPropriedades()
    {
        // Campos que são objetos não são retornados automaticamente do banco,
        // precisamos do Include() para que eles sejam incluidos
        return _context.PedidoFinal
            .Include(p => p.Cliente.Endereco).ThenInclude(endereco => endereco.Regiao)
            .Include(p => p.Acompanhamentos).ThenInclude(a => a.Acompanhamento)
            .Include(p => p.Pizzas).ThenInclude(p => p.Tamanho)
            .Include(p => p.Pizzas).ThenInclude(p => p.Sabores)
            .Include(p => p.Promocao);
    }

    private IQueryable<PedidoFinal> GetPedidoFinalComTodasAsPropriedades(int id)
    {
        return GetPedidosFinaisComTodasAsPropriedades()
            .Where(pedidoFinal => pedidoFinal.Id == id);
    }

    private async Task<Cliente> GetClienteComTodasAsPropriedades(string cpf)
    {
        return await _context.Cliente
            .Where(clienteNoBanco => clienteNoBanco.Cpf == cpf)
            .Include(c => c.Endereco.Regiao)
            .FirstOrDefaultAsync();
    }

    private async Task<List<AcompanhamentoPedido>> GetAcompanhamentosCompletos(List<AcompanhamentoPedido> acompanhamentos)
    {
        var acompanhamentosCompletos = new List<AcompanhamentoPedido>();
        // procura no banco e retorna nulo caso não exista ou já esteja associado a outro acompanhamento
        foreach (AcompanhamentoPedido acompanhamento in acompanhamentos)
        {
            var acompanhamentoCompleto = await _context.AcompanhamentoPedido
                .Where(acompBanco => acompBanco.Id == acompanhamento.Id)
                .Include(a => a.PedidoFinal)
                .FirstOrDefaultAsync();

            if (acompanhamentoCompleto == null) return null;
            if (acompanhamentoCompleto.PedidoFinal != null)
                return null;

            acompanhamentosCompletos.Add(acompanhamentoCompleto);
        }
        return acompanhamentosCompletos;
    }

    private async Task<List<PizzaPedido>> GetPizzasCompletas(List<PizzaPedido> pizzas)
    {
        var pizzasCompletas = new List<PizzaPedido>();
        // procura no banco e retorna nulo caso não exista ou já esteja associado a outro acompanhamento
        foreach (PizzaPedido pizza in pizzas)
        {
            var pizzaCompleta = await _context.PizzaPedido
                .Where(p => p.Id == pizza.Id)
                .Include(p => p.PedidoFinal)
                .FirstOrDefaultAsync();

            if (pizzaCompleta == null) return null;
            if (pizzaCompleta.PedidoFinal != null) return null;

            pizzasCompletas.Add(pizzaCompleta);
        }
        return pizzasCompletas;
    }
}
