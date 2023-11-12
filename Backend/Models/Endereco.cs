using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pizzaria;

public class Endereco
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Rua { get; set; }
    public int Numero { get; set; }
    public string Cep { get; set; }
    public Regiao Regiao { get; set; }
    public string ?Complemento { get; set; }
    [JsonIgnore]
    [ForeignKey("Cliente.Endereco")]
    public Cliente ?Cliente { get; set; }
    
    public Endereco() 
    {
        Rua = "";
        Numero = 0;
        Cep = "";
        Regiao = new Regiao();
    } 

    public Endereco(string rua, int numero, string cep, Regiao regiao, string ?complemento = null)
    {
        Rua = rua;
        Numero = numero;
        Cep = cep;
        Complemento = complemento;
        Regiao = regiao;
    }

    public override string ToString()
    {
        return  $"Rua: {Rua} \n" +
                $"Numero: {Numero} \n" +
                $"Complemento: {Complemento} \n" +
                $"CEP: {Cep} \n" +
                $"Regiao: {Regiao} \n";
    }

}