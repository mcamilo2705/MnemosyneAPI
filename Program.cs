using Microsoft.Extensions.Options;
using MnemosyneAPI.Context;

var builder = WebApplication.CreateBuilder(args);

//definindo o nome do arquivo do banco de dados
var stringConexao = "Data Source=app.db";
//Informando para usar sqlite, com base na classe do context
builder.Services.AddSqlite<MemoryDbContext>(stringConexao);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
