using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace pizzaria;

public class Regiao
{
    private int _id;
    private string _nome;
    private double _preco;

    public Regiao() { }

    public Regiao(string nome, double preco)
    {
        _nome = nome;
        _preco = preco;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Nome
    {
        get => _nome;
        set => _nome = value;
    }

    public double Preco
    {
        get => _preco;
        set => _preco = value;
    }

    public override string ToString()
    {
        return $"Região: {_nome} | Preço Frete: R${_preco}";
    }

}