using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Pizzaria.Models;

[PublicAPI]
public class Regiao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MaxLength(50, ErrorMessage = "O nome não deve ultrapassar 50 caracteres")]
    public string Nome { get; set; } = "";
     
    private Regiao() { }

    public Regiao(string nome)
    {
        Nome = nome;
    }

    public override string ToString()
    {
        return $"Região: {Nome}";
    }

}