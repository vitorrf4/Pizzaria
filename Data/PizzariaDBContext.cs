using Microsoft.EntityFrameworkCore;

namespace pizzaria;

public class PizzariaDBContext : DbContext {
    public DbSet<Cliente> Cliente { get; set; } // cada DbSet<> será uma tabela no banco

    public PizzariaDBContext() 
    {
        Database.EnsureCreated(); // garante que a database seja criada caso não exista
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSqlite(connectionString: "DataSource=pizzaria.db;Cache=shared");
    }
}