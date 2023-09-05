namespace pizzaria;
using System;
using System.Collections;

public class PizzaPedido 
{
    private int id;
    private List<Sabor> sabores = new();
    private double preco;
    private Tamanho tamanho;

    public PizzaPedido() { }

    public PizzaPedido(List<Sabor> sabores, Tamanho tamanho)
    {
        this.sabores = sabores;
        this.tamanho = tamanho;
        calcularPreco();
    }

    public int Id
    {
        get => id;
        set => id = value;
    }

    public List<Sabor> Sabores
    {
        get => sabores;
        set => sabores = value;
    }

    public double Preco
    {
        get => preco;
        set => preco = value;
    }

    public Tamanho Tamanho
    {
        get => tamanho;
        set => tamanho = value;
    }

    public void calcularPreco()
    {
        // preco = ((sabor1 + sabor2...saborN) / quantidade de sabores) + (preco * tamanho)

        double precoTotal = 0;

        foreach(Sabor sabor in sabores)
        {
            precoTotal += sabor.Preco;
        }

        if (sabores.Count > 0)
            precoTotal = precoTotal / sabores.Count;

        precoTotal += precoTotal * tamanho.Preco;

        this.preco = precoTotal;
    }

    override public string ToString()
    {
        Console.Write($"Pedido {id} | ");
        Console.Write($"Sabores: ");
        sabores.ForEach(s => Console.Write(s.Nome + ", "));
        Console.Write($"| Tamanho: {tamanho.Nome} | ");
        Console.Write($"Preço: {preco}");

        return "";
    }
}