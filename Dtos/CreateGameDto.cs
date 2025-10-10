namespace GameStore.api.Dtos;

public record class CreateGameDto(
    string Name,
    String Genre,
    decimal Price,
    DateOnly ReleaseDate
);
