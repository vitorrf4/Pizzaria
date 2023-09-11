using System.Security.Cryptography;

namespace pizzaria;

public class Tamanho
{
    private string _nome;
    private int _qntdFatias;
    private double _preco;

    public Tamanho() { }

    public Tamanho(string nome, int qntdFatias,double preco) 
    { 
        _nome = nome;
        _qntdFatias = qntdFatias;
        _preco = preco;
    }

    public string Nome
    {
        get => _nome;
        set => _nome = value;
    }

    public int QntdFatias
    {
        get => _qntdFatias;
        set => _qntdFatias = value;
    }

    public double Preco
    {
        get => _preco;
        set => _preco = value;
    }
    public override string ToString()
    {
        return $"Nome: {_nome} | Quantidade de Fatias: {_qntdFatias} | Preço: {_preco}";
    }
}