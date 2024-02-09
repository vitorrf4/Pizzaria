using Pizzaria.Data;
using Pizzaria.Middleware;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(); 
builder.Services.AddDbContext<PizzariaDbContext>();
builder.Services.AddTransient<PizzariaDbContext>();

builder.Services.AddCors();
var app = builder.Build();

var scope = app.Services.CreateScope().ServiceProvider;
var dbContext = scope.GetRequiredService<PizzariaDbContext>();

dbContext.InicializaValores();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next();
});
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseCors(opcoes => opcoes.AllowAnyOrigin().AllowAnyHeader());
app.UseAuthorization();
app.MapControllers();
app.Run();