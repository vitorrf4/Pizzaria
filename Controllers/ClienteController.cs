using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    private readonly ILogger<ClienteController> _logger;
    private PizzariaDBContext _context;

    public ClienteController(PizzariaDBContext context, ILogger<ClienteController> logger) 
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Cliente>>> Listar()
    {
        if (_context.Cliente is null)
            return NotFound();
        
        return await _context.Cliente.ToListAsync();
    }

    [HttpGet]
    [Route("listar/{cpf}")]
    public async Task<ActionResult<string>> Buscar([FromRoute] string cpf)   
    {
        var cliente = await _context.Cliente.FindAsync(cpf);
        if (cliente == null)
            return NotFound();
        
        return cliente.ToString();
    }

    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar(Cliente cliente)
    {
        _context.Add(cliente);
        _context.SaveChanges();
        return Created("", cliente);
    }
}
