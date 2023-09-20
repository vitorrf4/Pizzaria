using Microsoft.EntityFrameworkCore;
namespace pizzaria;

public class PizzariaDBContext : DbContext
{
    public DbSet<Acompanhamento> Acompanhamento { get; set; }
    public DbSet<AcompanhamentoPedido> AcompanhamentoPedido { get; set; }
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<PizzaPedido> PizzaPedido { get; set; }
    public DbSet<PedidoFinal> PedidoFinal { get; set; }
    public DbSet<Regiao> Regiao { get; set; }
    public DbSet<Sabor> Sabor { get; set; }
    public DbSet<Tamanho> Tamanho { get; set; }

    public PizzariaDBContext() 
    {
    }

    protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSqlite(connectionString: "DataSource=pizzaria.db;Cache=shared");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<PizzaPedido>()
        //    .HasMany(p => p.Sabores)
        //    .WithMany(s => s.PizzaPedidos);

        //modelBuilder.Entity<PedidoFinal>()
        //    .HasMany(p => p.Acompanhamentos)
        //    .WithMany(a => a.Pedidos);

        //modelBuilder.Entity<PedidoFinal>()
        //    .HasMany(p => p.Pizzas)
        //    .WithMany(p => p.Pedidos);



    }
}