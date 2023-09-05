using Microsoft.AspNetCore.Mvc;
//using API_Estacionamento.Models

[ApiController]
[Route("[controller]")]
public class CarroController : ControllerBase
{
    // private readonly ILogger<CarroController> _logger;
    // private static List<Carro> carros = new();
    // public CarroController(ILogger<CarroController> logger)
    // {
    //     _logger = logger;
    // }

    // [HttpGet()]
    // [Route("listar")]
    // public IActionResult Listar()
    // {
    //     return Ok(carros);
    // }
    // POST: API/carro/cadastrar

    // [HttpPost()]
    // [Route("cadastrar")]
    // public IActionResult Cadastrar(Carro carro) {
    //     carros.Add(carro);
    //     return Created("", carro);
    // }  

    // [HttpGet()]
    // [Route("listar/{placa}")]
    // public IActionResult Buscar(string placa)
    // {
    //     Carro carroEncontrado = carros.Find(x => x.Placa.Equals(placa));

    //     if (carroEncontrado == null) {
    //         return NotFound();
    //     }

    //     return Ok(carroEncontrado);
    // } 
}
