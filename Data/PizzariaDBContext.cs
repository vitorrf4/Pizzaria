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
    public DbSet<Endereco> Endereco { get; set; }
    public DbSet<Promocao> Promocao { get; set; }

    public PizzariaDBContext() 
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSqlite(connectionString: "DataSource=pizzaria.db;Cache=shared");
        optionsBuilder.EnableSensitiveDataLogging();
        
    }

    public void InicializaValoresTeste()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();

        //endereços
        var endereco1 = new Endereco("Rua 1", 1, "11111-11", "nenhum", "cidade 1", "estado 1");
        var endereco2 = new Endereco("Rua 2", 2, "11111-11", "casa 5", "cidade 2", "estado 2");

        //clientes
        var cliente1 = new Cliente("12345", "joao", "1111-1111" ,DateOnly.Parse("29/09/1973"), endereco1);
        var cliente2 = new Cliente("67890", "maria", "2222-2222", DateOnly.Parse("12/05/2000"), endereco2);
        endereco1.Cliente = cliente1;
        endereco2.Cliente = cliente2;

        // criar sabores => Sabor(int id, string nome, double preco)
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

        //cria acompanhamentoPedido
        var acompanhamentoPedido1 = new AcompanhamentoPedido(1, refrigerante, 3);
        var acompanhamentoPedido2 = new AcompanhamentoPedido(2, refrigerante, 3);

        //cria pizza pedido
        var sabores = new List<Sabor>() { frango };
        var pizzaPedido = new PizzaPedido(sabores, media);
        var pizzaPedido2 = new PizzaPedido(sabores, grande);

        // relaciona endereco e regiao
        endereco1.Regiao = centro;
        endereco2.Regiao = boqueirao;

        //cria pedido final
        var pedidoFinal = new PedidoFinal(
            cliente1,
            new List<PizzaPedido>() { pizzaPedido },
            new List<AcompanhamentoPedido>() { acompanhamentoPedido1 });
         
        //Adiciona no Banco
        Acompanhamento.AddRange(refrigerante, suco, paoDeAlho);
        Tamanho.AddRange(broto, pequena, media, grande);
        Sabor.AddRange(frango, calabresa, quatroQueijos);
        Regiao.AddRange(boqueirao, aguaVerde, centro);
        Endereco.AddRange(endereco1, endereco2);
        Cliente.AddRange(cliente1, cliente2);
        AcompanhamentoPedido.AddRange(acompanhamentoPedido1, acompanhamentoPedido2);
        PizzaPedido.AddRange(pizzaPedido, pizzaPedido2);
        PedidoFinal.Add(pedidoFinal);

        SaveChanges();
    }
}