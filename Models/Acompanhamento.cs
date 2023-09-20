using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzaria;

public class Acompanhamento
{
    private int _id;
    private string _nome;
    private double _preco;

    public Acompanhamento() { }

    public Acompanhamento(string nome, double preco)
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
        return $"ID: {_id} | Nome: {_nome} | Preço: {_preco}";
    }
}