using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MnemosyneAPI.Context;
using MnemosyneAPI.Endpoint;
using MnemosyneAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

//var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
//builder.WebHost.UseUrls($"http://*:{port}");

builder.Services.AddHealthChecks();

//inicio para configurar do cors
var MinhasOrigens = "_minhasOrigens";

builder.Services.AddCors(
    options => {
        options.AddPolicy(
            name: MinhasOrigens,
            policy => {
                policy.WithOrigins("http://localhost:5173");
            }
        );
    }
);

//fim de configuracao do cors

builder.Services.AddValidatorsFromAssemblyContaining<MemoryValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mnomosyne API",
        Version = "v1",
        Description = "API desenvolvida no curso de Programacao com C#, para atender ao FrontEnd do site Mnemosyne"
    });
});


//definindo o nome do arquivo do banco de dados
var stringConexao = "Data Source=app.db";
//Informando para usar sqlite, com base na classe do context
builder.Services.AddSqlite<MemoryDbContext>(stringConexao);

var app = builder.Build();

app.UseHealthChecks("/health");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mnemosyne API v1");
    });
}

app.MapMemoriesEndpoints();

app.UseCors();

app.Run();
