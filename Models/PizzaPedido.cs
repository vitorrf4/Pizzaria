namespace pizzaria;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class PizzaPedido 
{
    private int _id;
    private double _preco;
    private Tamanho _tamanho;
    private List<Sabor> _sabores = new();
    [JsonIgnore]
    public List<PedidoFinal> Pedidos { get; set; }

    public PizzaPedido() { }

    public PizzaPedido(List<Sabor> sabores, Tamanho tamanho)
    {
        _sabores = sabores;
        _tamanho = tamanho;
        CalcularPreco();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public List<Sabor> Sabores
    {
        get => _sabores;
        set => _sabores = value;
    }

    public double Preco
    {
        get => _preco;
        set => _preco = value;
    }

    public Tamanho Tamanho
    {
        get => _tamanho;
        set => _tamanho = value;
    }

    private double CalcularPreco()
    {
        // PREÇO TOTAL = ((sabor1 + sabor2...saborN) / quantidade de sabores) + (preco * tamanho)

        double precoTotal = 0.0;

        foreach(Sabor sabor in _sabores)
        {
            precoTotal += sabor.Preco;
        }

        if (_sabores.Count > 0)
        {
            precoTotal = precoTotal / _sabores.Count;
        }

        precoTotal += precoTotal * (_tamanho.Preco / 100.0);

        _preco = precoTotal;
        return _preco;
    }

    override public string ToString()
    {
        Console.Write($"Sabores: ");
        for (int i = 0; i < _sabores.Count; i++)
        {
            Console.Write(_sabores[i].Nome);
            if (i != (_sabores.Count - 1))
                Console.Write(", ");
        }

        Console.Write($" | Tamanho: {_tamanho.Nome} | ");
        Console.Write($"Preço: R${_preco}");

        return "";
    }
}