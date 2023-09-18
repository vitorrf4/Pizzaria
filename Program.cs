using pizzaria;

// A classe WebApplication cuida da criação e configuração do site, e o CreateBuilder instância um objeto dessa classe com as configurações padrão 
var builder = WebApplication.CreateBuilder(args); // faremos a configuração do site através dos metodos da variável builder

builder.Services.AddControllers(); // adiciona as classes controllers
builder.Services.AddSwaggerGen(); // adiciona o swagger
builder.Services.AddDbContext<PizzariaDBContext>(); // adiciona a nossa classe de banco de dados

var app = builder.Build(); // constrói a aplicação com as configurações do builder

if (app.Environment.IsDevelopment()) // swagger só será usado se estivermos no ambiente de desenvolvimento(ambiente padrão) do site
{
    app.UseSwagger(); // habilita o swagger padrão descrito em json, | http://localhost:5000/swagger/v1/swagger.json 
    app.UseSwaggerUI(); // habilita a versão visual do swagger | http://localhost:5000/swagger/index.html
}

app.MapControllers(); // delega a função de mapear as rotas para os controllers ( através do atributo [Route()] )
app.Run();