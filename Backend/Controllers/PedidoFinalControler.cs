using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("pedido-final/")]
public class PedidoFinalController : ControllerBase
{
    private PizzariaDBContext _context;

    public PedidoFinalController(PizzariaDBContext context)
    {
        _context = context;
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
        var pedidoFinal = await GetPedidoFinalComTodasAsPropriedades(id)
                                .FirstOrDefaultAsync();

        if (pedidoFinal == null)
            return NotFound();

        return Ok(pedidoFinal);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(PedidoFinal pedidoFinal)
    {
        // A função Attach comunica que um campo já está no banco de dados e não precisa ser inserido novamente
        // sem ela, o entity framework tenta adicionar um campo com um ID existente e da erro
        AttachCampos(pedidoFinal);

        pedidoFinal.CalcularPrecoTotal();
        pedidoFinal.HoraPedido = DateTime.Now;

        await _context.AddAsync(pedidoFinal);
        await _context.SaveChangesAsync();
        return Created("", pedidoFinal);
    }

    private void AttachCampos(PedidoFinal pedidoFinal)
    {
        // Attach cliente
        _context.Cliente.Attach(pedidoFinal.Cliente);

        // Attach tamanho e sabores
        pedidoFinal.Pizzas.ForEach(p =>
        {
            _context.Tamanho.Attach(p.Tamanho);
            _context.Sabor.AttachRange(p.Sabores);
        });

        // Attach acompanhamentos
        pedidoFinal.Acompanhamentos?.ForEach(a =>
        {
            _context.Acompanhamento.Attach(a.Acompanhamento);
        });

    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar(PedidoFinal pedidoFinal)
    {
        var pedidoNoBanco = await _context.PedidoFinal.FindAsync(pedidoFinal.Id);
        if (pedidoNoBanco == null) 
            return NotFound("Pedido nao encontrado");

        var clienteCompleto = await GetClienteComTodasAsPropriedades(pedidoFinal.Cliente.Cpf);
        if (clienteCompleto == null) 
            return BadRequest("O cliente nao foi encontrado");

        pedidoFinal.Cliente = clienteCompleto;

        if (pedidoFinal.Acompanhamentos != null)
        {
            var acompanhamentosCompletos = await GetAcompanhamentosCompletos(pedidoFinal.Acompanhamentos);
            if (acompanhamentosCompletos == null) 
                return BadRequest("O acompanhamento pedido nao foi encontrado");

            pedidoFinal.Acompanhamentos = acompanhamentosCompletos;
        }

        var pizzasCompletas = await GetPizzasCompletas(pedidoFinal.Pizzas);
        if (pizzasCompletas == null) return BadRequest("Pizza pedido invalido");
        pedidoFinal.Pizzas = pizzasCompletas;

        // linha necessaria para o _context nao dar erro de conflito
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

        if (pedidoFinal == null) return NotFound("Pedido n�o encontrado");

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

    [HttpGet]
    [Route("cliente/{cpf}")]
    public async Task<IActionResult> ListarPedidosPorCliente([FromRoute] string cpf) {
        var pedidos = await GetPedidosFinaisComTodasAsPropriedades()
            .Where(p => p.Cliente.Cpf == cpf)
            .ToListAsync();

        if (pedidos.Count == 0) return NotFound("Nenhum pedido encontrado");

        return Ok(pedidos);
    }

    // Helpers

    private IQueryable<PedidoFinal> GetPedidosFinaisComTodasAsPropriedades()
    {
        // Campos que s�o objetos n�o s�o retornados automaticamente do banco,
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
        // procura no banco e retorna nulo caso n�o exista ou j� esteja associado a outro acompanhamento
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
        // procura no banco e retorna nulo caso n�o exista ou j� esteja associado a outro acompanhamento
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
