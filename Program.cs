using pizzaria;
using Pizzaria.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<PizzariaDBContext>();
builder.Services.AddTransient<DbInitializer>();

var app = builder.Build();
using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var initializer = services.GetRequiredService<DbInitializer>();

initializer.InicializaValoresTesteBanco();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }
