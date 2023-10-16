using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pizzaria;

public class Regiao
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public double Preco { get; set; }
    [JsonIgnore]
    public List<Endereco> ?Enderecos { get; set; }
     
    public Regiao() { }

    public Regiao(int id, string nome, double preco)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
    }

    public override string ToString()
    {
        return $"Região: {Nome} | Preço Frete: R${Preco}";
    }

}