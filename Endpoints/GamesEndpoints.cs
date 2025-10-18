using System;
using GameStore.api.Dtos;
namespace GameStore.api.Endpoints;

public static class GamesEndpoints
{
 const string GetGameName = "GetGame";
    private static readonly List<GamesDto> games = [
        new (
            1,
            "Street Fighter II",
            "Fighting",
            19.99M,
            new DateOnly(1992, 7, 15)),
        new (
            2,
            "Final Fantasy XIV",
            "Roleplaying",
            59.99M,
            new DateOnly(2010, 9, 30)
        ),
        new (
            3,
            "The Legend of Zelda: Breath of the Wild",
            "Action-Adventure",
            59.99M,
            new DateOnly(2017, 3, 3)
        ),
        new (
            4,
            "Super Mario 64",
            "Platformer",
            39.99M,
            new DateOnly(1996, 6, 23)
        ),
        new (
            5,
            "Halo: Combat Evolved",
            "Shooter",
            29.99M,
            new DateOnly(2001, 11, 15)
        ),
        new (
            6,
            "The Witcher 3: Wild Hunt",
            "Roleplaying",
            49.99M,
            new DateOnly(2015, 5, 19)
        ),
        new (
            7,
            "Minecraft",
            "Sandbox",
            26.95M,
            new DateOnly(2011, 11, 18)
        ),
        new (
            8,
            "DOOM",
            "Shooter",
            14.99M,
            new DateOnly(1993, 12, 10)
        ),
        new (
            9,
            "Portal 2",
            "Puzzle",
            19.99M,
            new DateOnly(2011, 4, 19)
        ),
        new (
            10,
            "Elden Ring",
            "Action RPG",
            59.99M,
            new DateOnly(2022, 2, 25)
        )
    ];

    public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();
        //Get /games
        group.MapGet("/", () => games);

        //Get games by id
        group.MapGet("/{id}", (int id) =>
        {
            GamesDto? game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameName);

        //POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {

            GamesDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);
            return Results.CreatedAtRoute(GetGameName, new { id = game.Id }, game);
        }).WithParameterValidation();

        //PUT /games
        group.MapPut("/{id}", (int id, UpdateGameDto updateGameDto) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            if(index == -1)
            {
                return Results.NotFound();
            }
            games[index] = new GamesDto(
                id,
                updateGameDto.Name,
                updateGameDto.Genre,
                updateGameDto.Price,
                updateGameDto.ReleaseDate
            );
            return Results.NoContent();
        });

        //DELETE /games
        group.MapDelete("games/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
        return group;
    }
}
