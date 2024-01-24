using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzaria;

public class Endereco
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Rua { get; set; } = "";
    public int Numero { get; set; }
    public string Cep { get; set; } = "";
    public Regiao Regiao { get; set; } = new Regiao();
    public string ?Complemento { get; set; }
    public string ClienteCpf { get; set; } = "";
    
    public Endereco() { } 

    public Endereco(string rua, int numero, string cep, Regiao regiao, string ?complemento = null)
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