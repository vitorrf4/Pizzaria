using pizzaria;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(); 
builder.Services.AddDbContext<PizzariaDBContext>();
builder.Services.AddTransient<PizzariaDBContext>();

builder.Services.AddCors();
var app = builder.Build();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var DbContext = services.GetRequiredService<PizzariaDBContext>();

DbContext.InicializaValores();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseCors(opcoes => opcoes.AllowAnyOrigin().AllowAnyHeader());
app.UseAuthorization();
app.MapControllers();
app.Run();