using pizzaria;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(); 
builder.Services.AddDbContext<PizzariaDBContext>();
builder.Services.AddTransient<PizzariaDBContext>();

builder.Services.AddCors();
var app = builder.Build();

var scope = app.Services.CreateScope().ServiceProvider;
var DbContext = scope.GetRequiredService<PizzariaDBContext>();

DbContext.InicializaValores();

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