using System.ComponentModel.DataAnnotations;

namespace pizzaria;

public class Promocao
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public double Desconto { get; set; }

    public Promocao() { }

    public Promocao(int id, string nome, double desconto)
    {
        Id = id;
        Nome = nome;
        Desconto = desconto;
    }

    public override string ToString()
    {
        return $"Nome da Promoção: {Nome} | Porcentagem de Desconto: {Desconto}%";
    }
}