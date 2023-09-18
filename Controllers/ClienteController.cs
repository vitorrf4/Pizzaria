using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace pizzaria;

// ApiController habilita funções utéis para API como validação automática de um json malformado, 
// inferir se um parametro vem da rota ou do corpo, mensagens de erros detalhadas, entre outros
[ApiController] 
[Route("cliente/")] // todos os métodos dessa classe estarão no caminho /cliente/... , ex: /cliente/buscar, /cliente/cadastrar
public class ClienteController : ControllerBase // nosso controller precisa herdar da classe ControllerBase pra termos acesso as funcionalidades
                                                // relacionadas ao controller como ActionResult, atributos [Route], [HttpGet] , etc
{
    private readonly ILogger<ClienteController> _logger; // classe usada para logar informações, debug, erros
    private PizzariaDBContext _context; // classe que acessa o banco de dados

    public ClienteController(PizzariaDBContext context, ILogger<ClienteController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet()] // define que o método dessa rota é GET
    [Route("buscar")] // define o caminho dessa rota como "buscar", caminho completo: https://localhost:5000/cliente/buscar
    public async Task<ActionResult<IEnumerable<Cliente>>> ListarTodos()
    {   //                      ^
        // Task = uma ação que será executada de forma assíncrona
        // ActionResult = uma classe genérica com vários subtipos, nos permitindo retornar diferentes tipos de dados de acordo com a situação,
        // aqui usaremos o subtipo HttpStatusCodeResult relacionado aos requests ao servidor (404 Not Found, 200 Ok, etc) 
        // para dizer qual foi o resultado da requisição, e ainda especificamos que o resultado será do tipo IEnumerable<Cliente>
        // IEnumerable = qualquer tipo de variável que contém mais de um valor, lista, array, map, etc. 
        // nesse caso retornaremos uma lista de objetos Cliente

        List<Cliente> clientes;

        try {
            clientes = await _context.Cliente.ToListAsync(); // procura no banco(_context) todos os clientes  e transforma em uma lista
            return Ok(clientes); // retorna um ActionResult do tipo Ok com a lista de clientes
        } catch (SqliteException) {
            // caso a tabela não exista será lançada a exceção SqLiteException e o metódo retornará um ActionResult do tipo NotFound
            return NotFound("A tabela Cliente não existe"); 
        }
    }

    [HttpGet()]
    [Route("buscar/{cpf}")]
     // a variável "string cpf" será o {cpf} que foi mandado na url da rota, ex: https://localhost:5000/cpf/buscar/123456789-10
    public async Task<ActionResult<Cliente>> BuscarPorCPF(string cpf)
    {
        var cliente = await _context.Cliente.FindAsync(cpf);

        if (cliente == null)
            return NotFound("Nenhum cliente com esse CPF encontrado");
        
        return Ok(cliente);
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult<Cliente>> Cadastrar(Cliente cliente) // Como é um tipo complexo, o objeto Cliente virá do corpo da requisiçao, não da url
    {
        if (_context.Cliente.Contains(cliente))
            return Conflict("Um cliente com esse CPF já está cadastrado");

        await _context.AddAsync(cliente); // adiciona o objeto Cliente mandado no corpo da requisição no banco
        await _context.SaveChangesAsync(); // salva as alterações no banco
        return Created("", cliente);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Cliente cliente)
    {
        _context.Cliente.Update(cliente);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("deletar/{cpf}")]
    public async Task<ActionResult> Deletar(string cpf)
    {
        var cliente = await _context.Cliente.FindAsync(cpf);
        if (cliente == null) return NotFound();

        _context.Cliente.Remove(cliente);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch]
    [Route("/mudar_telefone/{cpf}")]
    public async Task<ActionResult> MudarTelefone(string cpf, [FromBody] string telefone)
    {
        var cliente = await _context.Cliente.FindAsync(cpf);
        if (cliente == null) return NotFound();

        cliente.Telefone = telefone;
        await _context.SaveChangesAsync();
        return Ok(cliente);
    }
}
