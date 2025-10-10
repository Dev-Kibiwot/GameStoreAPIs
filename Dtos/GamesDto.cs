namespace GameStore.api.Dtos;

public record class GamesDto(
    int Id,
    string Name,
    String Genre,
    decimal price,
    DateOnly ReleaseDate
);
