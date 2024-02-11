using JetBrains.Annotations;

namespace Pizzaria.DTOs;

[PublicAPI]
public struct LoginDto
{
    public string Email { get; set; }
    public string Senha { get; set; }

    public LoginDto(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }
}