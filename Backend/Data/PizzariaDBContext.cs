using Microsoft.EntityFrameworkCore;

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

    public PizzariaDBContext() 
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
        modelBuilder.Entity<PizzaPedido>()
                    .HasMany(p => p.Sabores)
                    .WithMany();
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
        var endereco1 = new Endereco("Rua 1", 1, "11111-11", centro);
        var endereco2 = new Endereco("Rua 2", 2, "11111-11", boqueirao, "casa 5");

        // Cliente
        var cliente1 = new Cliente("12345", "joao", "1111-1111",  endereco1);
        var cliente2 = new Cliente("67890", "maria", "2222-2222", endereco2);

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
        var pizzaPedido2 = new PizzaPedido(sabores, grande, 1);

        // PedidoFinal
        var pedidoFinal = new PedidoFinal(
            cliente1.Cpf, cliente1.Endereco,
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