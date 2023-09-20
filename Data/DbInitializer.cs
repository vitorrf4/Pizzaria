using pizzaria;

namespace Pizzaria.Data;

public class DbInitializer
{
    private readonly PizzariaDBContext _context;

    public DbInitializer(PizzariaDBContext context)
    {
        _context = context;
    }

    public void InicializaValoresTesteBanco()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        // criar clientes => Cliente(string cpf, string nome, string telefone)
        Cliente cliente1 = new Cliente("12345", "Nome 1", "1111-1111");
        Cliente cliente2 = new Cliente("67890", "Nome 2", "2222-2222");
        Cliente cliente3 = new Cliente("54321", "Nome 3", "3333-3333");

        // criar sabores => Sabor(string nome, double preco)
        var frango = new Sabor("Frango", 15.0);
        var calabresa = new Sabor("Calabresa", 17.0);
        var quatroQueijos = new Sabor("Quatro Queijos", 19.0);

        //criar tamanhos => Tamanho(string nome, int qntdFatias,double preco) 
        var broto = new Tamanho("BROTO", 4, 0.0);
        var pequena = new Tamanho("PEQUENA", 6, 50.0);
        var media = new Tamanho("MEDIA", 8, 100.0);
        var grande = new Tamanho("GRANDE", 10, 150.0);

        //criar regioes => Regiao(string nome, double preco)
        var centro = new Regiao("Centro", 5.0);
        var aguaVerde = new Regiao("Água Verde", 10.0);
        var boqueirao = new Regiao("Boqueirão", 30.0);

        //criar acompanhamentos => Acompanhamento(string nome, double preco)
        var refrigerante = new Acompanhamento("Refrigerante", 12.0);
        var suco = new Acompanhamento("Suco", 10.0);
        var paoDeAlho = new Acompanhamento("Pão de Alho", 25.0);

        //Adiciona no Banco
        _context.Acompanhamento.AddRange(refrigerante, suco, paoDeAlho);
        _context.Tamanho.AddRange(broto, pequena, media, grande);
        _context.Sabor.AddRange(frango, calabresa, quatroQueijos);
        _context.Cliente.AddRange(cliente1, cliente2, cliente3);
        _context.Regiao.AddRange(boqueirao, centro);

        _context.SaveChanges();
    }
}

