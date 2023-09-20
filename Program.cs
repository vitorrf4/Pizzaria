using pizzaria;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PizzariaDBContext>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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