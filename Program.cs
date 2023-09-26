using pizzaria;

// A classe WebApplication cuida da criação e configuração do site, e o CreateBuilder instância um objeto dessa classe com as configurações padrão 
var builder = WebApplication.CreateBuilder(args); // faremos a configuração do site através dos metodos da variável builder

builder.Services.AddControllers(); // adiciona as classes controllers
builder.Services.AddSwaggerGen(); // adiciona o swagger
builder.Services.AddDbContext<PizzariaDBContext>(); // adiciona a nossa classe de banco de dados

builder.Services.AddTransient<PizzariaDBContext>();

var app = builder.Build(); // constrói a aplicação com as configurações do builder

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var DbContext = services.GetRequiredService<PizzariaDBContext>();

//DbContext.InicializaValoresTeste(); // Delete esta linha se quiser que o banco inicie vazio

if (app.Environment.IsDevelopment()) // swagger só será usado se estivermos no ambiente de desenvolvimento(ambiente padrão) do site
{
    app.UseSwagger(); // habilita o swagger padrão descrito em json, | http://localhost:5000/swagger/v1/swagger.json 
    app.UseSwaggerUI(); // habilita a versão visual do swagger | http://localhost:5000/swagger/index.html
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

//Quando atualizar a controller deve-se executar os comandos abaixo no terminal
//Para atualizar as migrations:
//dotnet ef migrations add {migration_name}
//Para gerar o pacote
//dotnet build
//Para atualiza a base de dados
//dotnet ef database update 
//Para rodar a aplicação
//dotnet run
