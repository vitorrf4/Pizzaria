using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizzaria;
using System.Security.Cryptography;
using System.Text.Json.Serialization.Metadata;

[ApiController]
[Route("[controller]")]
public class PizzaPedidoController : ControllerBase
{
    private readonly ILogger<PizzaPedidoController> _logger;
    private PizzariaDBContext _context;

    public PizzaPedidoController(ILogger<PizzaPedidoController> logger, PizzariaDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult> Listar()
    {
        if (_context.PizzaPedido == null)
            return NotFound();

        var pedidos = await _context.PizzaPedido
            .Include("Sabores")
            .Include("Tamanho")
            .ToListAsync(); 

        return Ok(pedidos);
    }

    [HttpPost]
    [Route("pedir")]
    public async Task<ActionResult> Cadastrar(List<int> sabores, string tamanhoParametro)
    {
        tamanhoParametro = tamanhoParametro.ToUpper();

        List<Sabor> listaSabores = new();

        sabores.ForEach(s =>
            listaSabores.Add(_context.Sabor.Find(s))
        );

        var tamanho = await _context.Tamanho.FindAsync(tamanhoParametro);

        if (sabores == null || tamanho == null)
        {
            return BadRequest("Um dos valores é inválido");
        }

        var pedido = new PizzaPedido(listaSabores, tamanho);
        var pedidoBanco = _context.PizzaPedido.Add(pedido);
        _context.SaveChanges();

        return Created("", pedido);
    }
}

