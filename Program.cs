using System.Reflection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MnemosyneAPI.Context;
using MnemosyneAPI.Endpoint;

var builder = WebApplication.CreateBuilder(args);

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mnemosyne API v1");
    });
}

app.MapMemoriesEndpoints();

app.Run();
