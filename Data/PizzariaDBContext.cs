using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace pizzaria;

public class PizzariaDBContext : DbContext{
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<PizzaPedido> PizzaPedido { get; set; }
    public DbSet<PedidoFinal> PedidoFinal { get; set; }
    public DbSet<Acompanhamento> Acompanhamento { get; set; }
    public DbSet<Tamanho> Tamanho { get; set; }
    public DbSet<Sabor> Sabor { get; set; }
    public DbSet<Regiao> Regiao { get; set; }
    public DbSet<AcompanhamentoPedido> AcompanhamentoPedido { get; set; }

    public PizzariaDBContext() 
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSqlite(connectionString: "DataSource=pizzaria.db;Cache=shared");
    }

    public void InicializaValoresTeste()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();

        Cliente cliente1 = new Cliente("12345", "Nome 1", "1111-1111");
        Cliente cliente2 = new Cliente("67890", "Nome 2", "2222-2222");
        Cliente cliente3 = new Cliente("54321", "Nome 3", "3333-3333");

        // criar sabores => Sabor(string nome, double preco)
        var frango = new Sabor(1, "Frango", 15.0);
        var calabresa = new Sabor(2, "Calabresa", 17.0);
        var quatroQueijos = new Sabor(3, "Quatro Queijos", 19.0);

        //criar tamanhos => Tamanho(string nome, int qntdFatias,double preco) 
        var broto = new Tamanho("BROTO", 4, 0.0);
        var pequena = new Tamanho("PEQUENA", 6, 50.0);
        var media = new Tamanho("MEDIA", 8, 100.0);
        var grande = new Tamanho("GRANDE", 10, 150.0);

        //criar regioes => Regiao(string nome, double preco)
        var centro = new Regiao(1, "Centro", 5.0);
        var aguaVerde = new Regiao(2, "Água Verde", 10.0);
        var boqueirao = new Regiao(3, "Boqueirão", 30.0);

        //criar acompanhamentos => Acompanhamento(string nome, double preco)
        var refrigerante = new Acompanhamento(1, "Refrigerante", 12.0);
        var suco = new Acompanhamento(2, "Suco", 10.0);
        var paoDeAlho = new Acompanhamento(3, "Pão de Alho", 25.0);

        //cria pizza pedido
        var sabores = new List<Sabor>() { frango };
        
        var pizzaPedido = new PizzaPedido(sabores, media);

        //Adiciona no Banco
        Acompanhamento.AddRange(refrigerante, suco, paoDeAlho);
        Tamanho.AddRange(broto, pequena, media, grande);
        Sabor.AddRange(frango, calabresa, quatroQueijos);
        Cliente.AddRange(cliente1, cliente2, cliente3);
        Regiao.AddRange(boqueirao, aguaVerde, centro);
        PizzaPedido.Add(pizzaPedido);

        SaveChanges();
    }
}