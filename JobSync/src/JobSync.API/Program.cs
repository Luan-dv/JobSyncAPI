using JobSync.Api.Filters;
using JobSync.Api.Middleware;
using JobSync.Aplication;
using JobSync.Infrastucture;
using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:5500" , "http://127.0.0.1:5500") // porta do seu front-end (ajuste se necessário)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));  // consegue enxergar a exception

builder.Services.AddInfrastructure(builder.Configuration); // extensão para adicionar injeção de dependencia
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<CultureMiddleware>(); //configuração para usar middleware

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");


app.UseAuthorization();

app.MapControllers();

app.Run();
