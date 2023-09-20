using Microsoft.EntityFrameworkCore;

namespace pizzaria;

public class PizzariaDBContext : DbContext{
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<PedidoFinal> PedidoFinal { get; set; }

    public PizzariaDBContext()
    {
        //Database.EnsureDeleted();
        //Database.Migrate();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSqlite(connectionString: "DataSource=pizzaria.db;Cache=shared");
    }
}