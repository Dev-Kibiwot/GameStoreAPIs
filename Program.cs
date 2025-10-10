using GameStore.api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string GetGameName = "GetGame";
List<GamesDto> games = [
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

//Get /games
app.MapGet("games", () => games);

//Get games by id
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameName);

//POST /games
app.MapPost("games", (CreateGameDto newGame) =>
{
    GamesDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
    return Results.CreatedAtRoute(GetGameName,new{id = game.Id},game);
});

app.Run(); 