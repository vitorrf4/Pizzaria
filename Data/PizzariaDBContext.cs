using Microsoft.EntityFrameworkCore;

namespace pizzaria;

public class PizzariaDBContext : DbContext{
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<PedidoFinal> PedidoFinal { get; set; }
    public DbSet<Acompanhamento> Acompanhamento { get; set; }
    public DbSet<AcompanhamentoPedido> AcompanhamentoPedido { get; set; }
    public DbSet<Sabor> Sabor { get; set; }
    public DbSet<Tamanho> Tamanho { get; set; }

    public PizzariaDBContext() 
    {
        Database.EnsureCreated(); // garante que a database seja criada caso não exista
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSqlite(connectionString: "DataSource=pizzaria.db;Cache=shared");
    }
}