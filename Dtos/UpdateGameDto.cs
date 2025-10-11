namespace GameStore.api.Dtos;

public record class UpdateGameDto(
    string Name,
    String Genre,
    decimal Price,
    DateOnly ReleaseDate
);
