using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace Pizzaria.Models;

[PublicAPI]
public class Usuario 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyOrder(-3)]
    public int Id { get; set; }
    [JsonPropertyOrder(-2)]
    [Required(ErrorMessage = "O email é obrigatório")]
    [MaxLength(100, ErrorMessage = "O email não deve ultrapassar 100 caracteres")]
    public string Email { get; set; } = "";
    [JsonPropertyOrder(-1)]
    [Required(ErrorMessage = "A senha é obrigatória")]
    [MaxLength(100, ErrorMessage = "A senha não deve ultrapassar 100 caracteres")]
    public string Senha { get; set; } = "";

    protected Usuario() { }

    public Usuario(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }
}