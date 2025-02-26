﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MnemosyneAPI.Context;
using MnemosyneAPI.Model;

namespace MnemosyneAPI.Endpoint
{
    public static class MemoryEndpoints
    {
        public static void MapMemoriesEndpoints(this WebApplication app)
        {
            //Listas de memories
            app.MapGet("/memories", async (MemoryDbContext db) =>
            {
                return await db.Memories.ToListAsync();
            });

            //Listar memories por id
            app.MapGet("/memories/{id}", async (int id, MemoryDbContext db) =>
            {
                return await db.Memories.FindAsync(id)
                 //condicional de encontrar a memory ok, se nao encontrar retorna null e apresenta notfound   
                 is Memory memory
                     ? Results.Ok(memory)
                     : Results.NotFound();
            })
                //para documentar no swagger opcoes que tera de retorno
                .Produces<Memory>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                ;


            //Criar memories
            app.MapPost("/memories", async (Memory memory, MemoryDbContext db) =>
            {
                //verificando se a criacao esta nulla
                if (memory == null) return Results.BadRequest("Requisicao invalida");
                
                db.Memories.Add(memory);
                //para salvar a memories
                await db.SaveChangesAsync();

                return Results.Created($"memories/{memory.Id}", memory);
            })
                .Produces<Memory>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                ;

            //Atualizar memories
            app.MapPut("/memories/{id}", async (int id, Memory memory, MemoryDbContext db) =>
            {
                var memoriesEncontrada = await db.Memories.FindAsync(id);

                if (memoriesEncontrada is null) return Results.NotFound();

                memoriesEncontrada.Title = memory.Title;
                memoriesEncontrada.Description = memory.Description;
                memoriesEncontrada.Date = memory.Date;
                memoriesEncontrada.Images = memory.Images;
                await db.SaveChangesAsync();

                //return Results.Ok(memoriesEncontrada);
                return Results.NoContent();
            })
                .Produces<Memory>(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                ;

            //Deletar memories por id
            app.MapDelete("/memories/{id}", async (int id, MemoryDbContext db) =>
            {
                var memoriesEncontada = await db.Memories.FindAsync(id);

                if (memoriesEncontada is null) return Results.NotFound();

                db.Memories.Remove(memoriesEncontada);
                await db.SaveChangesAsync();
                return Results.Ok(memoriesEncontada);
            })
                .Produces<Memory>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                ;
        }
    }
}
