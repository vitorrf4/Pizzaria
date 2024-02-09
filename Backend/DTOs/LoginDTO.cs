namespace pizzaria;

public class LoginDTO
{
    public string Email { get; set; }
    public string Senha { get; set; }

    public LoginDTO(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }
}