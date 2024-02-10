using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Pizzaria.Models;

[PublicAPI]
public class Endereco
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required(ErrorMessage = "A rua é obrigatória")]
    [MaxLength(150, ErrorMessage = "A rua não deve ultrapassar 150 caracteres")]
    public string Rua { get; set; } = "";
    [Required(ErrorMessage = "O número é obrigatório")]
    [MaxLength(8, ErrorMessage = "O número não deve ultrapassar 8 caracteres")]
    public string Numero { get; set; }
    [Required(ErrorMessage = "O CEP é obrigatório")]
    [MaxLength(8, ErrorMessage = "O CEP não deve ultrapassar 8 caracteres")]
    public string Cep { get; set; } = "";
    [Required(ErrorMessage = "A região é obrigatória")]
    public Regiao Regiao { get; set; }
    [MaxLength(50, ErrorMessage = "O Complemento não deve ultrapassar 50 caracteres")]
    public string ?Complemento { get; set; }
    [Required(ErrorMessage = "O ClienteId é obrigatório")]
    public int ClienteId { get; set; }
    
    private Endereco() { } 

    public Endereco(string rua, string numero, string cep, Regiao regiao, string ?complemento = null)
    {
        Rua = rua;
        Numero = numero;
        Cep = cep;
        Regiao = regiao;
        Complemento = complemento;
    }

    public override string ToString()
    {
        return  $"Rua: {Rua} \n" +
                $"Numero: {Numero} \n" +
                $"Complemento: {Complemento ?? "Nenhum"} \n" +
                $"CEP: {Cep} \n" +
                $"Regiao: {Regiao} \n";
    }

}