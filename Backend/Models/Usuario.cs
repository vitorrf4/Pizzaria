using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzaria;

public class Usuario 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string Senha { get; set; } = "";

    public Usuario() { }

    public Usuario(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }
}