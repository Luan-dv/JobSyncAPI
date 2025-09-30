using JobSync.Api.Filters;
using JobSync.Api.Middleware;
using JobSync.Infrastucture;
using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));  // consegue enxergar a exception

builder.Services.AddInfrastructure(builder.Configuration); // extensão para adicionar injeção de dependencia

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<CultureMiddleware>(); //configuração para usar middleware

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
