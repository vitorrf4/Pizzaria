using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzaria.Models;

public class Regiao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Nome { get; set; } = "";
     
    public Regiao() { }

    public Regiao(string nome)
    {
        Nome = nome;
    }

    public override string ToString()
    {
        return $"Região: {Nome}";
    }

}