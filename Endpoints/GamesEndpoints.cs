using System;
using GameStore.api.Data;
using GameStore.api.Dtos;
using GameStore.api.Entities;
using GameStore.api.Mapping;
using Microsoft.EntityFrameworkCore;
namespace GameStore.api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameName = "GetGame";
    public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();
        //Get /games
        group.MapGet("/",async (GameStoreContext dbConext) =>
          await dbConext.Games.Include(
                games => games.Genre).Select(
                    games => games.ToGameSummaryDto()).AsNoTracking().ToListAsync()
        );

        //Get games by id
        group.MapGet("/{id}",async (int id, GameStoreContext dbContext) =>
        {
            Game? game =await dbContext.Games.FindAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        }).WithName(GetGameName);

        //POST /games
        group.MapPost("/",async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();
            dbContext.Games.Add(game);
           await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute(
                GetGameName,
                new { id = game.Id },
                game.ToGameDetailsDto()
             );
        }).WithParameterValidation();

        //PUT /games
        group.MapPut("/{id}",async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame =await dbContext.Games.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }
            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        //DELETE /games
        group.MapDelete("/{id}",async (int id, GameStoreContext dbContext) =>
        {
           await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });
        return group;
    }
}
