using System.ComponentModel.DataAnnotations;

namespace pizzaria;

public class Usuario 
{
    [Key]
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string Senha { get; set; } = "";

    public Usuario(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }
}