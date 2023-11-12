using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pizzaria;

public class Regiao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Nome { get; set; }
    [JsonIgnore]
    public List<Endereco> ?Enderecos { get; set; }
     
    public Regiao() 
    { 
        Nome = "";
    }

    public Regiao(string nome)
    {
        Nome = nome;
    }

    public override string ToString()
    {
        return $"Região: {Nome}";
    }

}