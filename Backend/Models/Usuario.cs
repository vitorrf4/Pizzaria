using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pizzaria;

public class Usuario 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyOrder(-3)]
    public int Id { get; set; }
    [JsonPropertyOrder(-2)]
    public string Email { get; set; } = "";
    [JsonPropertyOrder(-1)]
    public string Senha { get; set; } = "";

    public Usuario() { }

    public Usuario(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }
}