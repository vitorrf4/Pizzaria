namespace Pizzaria.Models;

public class Resultado<T>
{
    public bool TemErros { get; }
    public string? Mensagem { get; }
    public T? Data { get; }
    
    private Resultado(bool temErros, string? mensagem = "", T? data = default)
    {
        TemErros = temErros;
        Mensagem = mensagem;
        Data = data;
    }

    public static Resultado<T> Sucesso(T? data = default)
    {
        return new Resultado<T>(false, data: data);
    }
    
    public static Resultado<T> Falha(string? mensagem = "")
    {
        return new Resultado<T>(true, mensagem);
    }
}