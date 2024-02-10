using Microsoft.EntityFrameworkCore;
using Pizzaria.Models;

namespace Pizzaria.Data;

public sealed class PizzariaDbContext : DbContext{
    public required DbSet<Usuario> Usuario { get; init; }
    public required DbSet<Cliente> Cliente { get; init; }
    public required DbSet<PizzaPedido> PizzaPedido { get; init; }
    public required DbSet<PedidoFinal> PedidoFinal { get; init; }
    public required DbSet<Acompanhamento> Acompanhamento { get; init; }
    public required DbSet<Tamanho> Tamanho { get; init; }
    public required DbSet<Sabor> Sabor { get; init; }
    public required DbSet<Regiao> Regiao { get; init; }
    public required DbSet<AcompanhamentoPedido> AcompanhamentoPedido { get; init; }
    public required DbSet<Endereco> Endereco { get; init; }

    public PizzariaDbContext() 
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSqlite(connectionString: "DataSource=pizzaria.db;Cache=shared");
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
                    .HasDiscriminator<string>("tipo_usuario")
                    .HasValue<Usuario>("usuario")
                    .HasValue<Cliente>("cliente");

        modelBuilder.Entity<PizzaPedido>()
                    .HasMany(p => p.Sabores)
                    .WithMany();

        modelBuilder.Entity<PedidoFinal>()
                    .HasOne<Cliente>()
                    .WithMany()
                    .HasForeignKey(c => c.ClienteId);
    }

    public void InicializaValores()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();

        // Regiao
        var centro = new Regiao("Centro");
        var aguaVerde = new Regiao("Água Verde");
        var boqueirao = new Regiao("Boqueirão");
        var capaoRaso = new Regiao("Capão Raso");

        // Endereco
        var endereco1 = new Endereco("Rua 1", "1", "11111-11", centro);
        var endereco2 = new Endereco("Rua 2", "2", "11111-11", boqueirao, "casa 5");

        // Cliente
        var cliente1 = new Cliente("joao@","teste1" , "joao", "1111-1111",  endereco1);
        var cliente2 = new Cliente("maria@","teste2" ,"maria", "2222-2222", endereco2);

        // Sabor
        var frango = new Sabor("Frango", 15.0);
        var portuguesa = new Sabor("Portuguesa", 15.0);
        var muzzarela = new Sabor("Muzzarela", 15.0);
        var calabresa = new Sabor("Calabresa", 15.0);
        var quatroQueijos = new Sabor("Quatro Queijos", 17.0);
        var brocolisBacon = new Sabor("Brocolis com Bacon", 17.0);
        var camarao = new Sabor("Camarão", 19.0);
        var champignon = new Sabor("Champignon", 19.0);
        var pepperoni = new Sabor("Pepperoni", 19.0);
        var brigadeiro = new Sabor("Brigadeiro", 19.0);
        var leiteCondensado = new Sabor("Leite Condensado", 19.0);

        // Tamanho 
        var broto = new Tamanho("Broto", 4, 1, 1.0);
        var pequena = new Tamanho("Pequena", 6, 1, 1.5);
        var media = new Tamanho("Media", 8, 2, 1.8);
        var grande = new Tamanho("Grande", 10, 2, 2.5);
        var familia = new Tamanho("Familia", 12, 3, 3);
        var gigante = new Tamanho("Gigante", 16, 4, 3.5);

        // Acompanhamento
        var refrigerante = new Acompanhamento("Coca Cola 2L", 12.0);
        var suco = new Acompanhamento("Suco Del Valle 500ml", 8.0);
        var agua = new Acompanhamento("Agua 500ml", 3.0);
        var chocolate = new Acompanhamento("Barra Diamante Negro", 7.0);
        var paoDeAlho = new Acompanhamento("Pão de Alho", 25.0);

        // AcompanhamentoPedido
        var acompanhamentoPedido1 = new AcompanhamentoPedido(refrigerante, 3);
        var acompanhamentoPedido2 = new AcompanhamentoPedido(suco, 1);

        // PizzaPedido
        var sabores = new List<Sabor>() { frango };
        var pizzaPedido = new PizzaPedido(sabores, media, 3);
        var pizzaPedido2 = new PizzaPedido(sabores, grande);

        // PedidoFinal
        var pedidoFinal = new PedidoFinal(
            1, cliente1.Endereco,
            new List<PizzaPedido>() { pizzaPedido, pizzaPedido2 },
            new List<AcompanhamentoPedido>() { acompanhamentoPedido1 });
         
        //Adiciona no Banco
        Acompanhamento.AddRange(refrigerante, suco, paoDeAlho, agua, chocolate);
        Tamanho.AddRange(broto, pequena, media, grande, familia, gigante);

        Sabor.AddRange(frango, calabresa, quatroQueijos, portuguesa, camarao, 
        muzzarela, brocolisBacon, champignon, pepperoni, brigadeiro, leiteCondensado);

        Regiao.AddRange(boqueirao, aguaVerde, centro, capaoRaso);
        Endereco.AddRange(endereco1, endereco2);
        Cliente.AddRange(cliente1, cliente2);
        AcompanhamentoPedido.AddRange(acompanhamentoPedido1, acompanhamentoPedido2);
        PizzaPedido.AddRange(pizzaPedido, pizzaPedido2);
        PedidoFinal.Add(pedidoFinal);

        SaveChanges();
    }
}