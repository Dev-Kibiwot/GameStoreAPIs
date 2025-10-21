namespace GameStore.api.Dtos;

public record class GamesDetailsDto(
    int Id,
    string Name,
    int GenreId,
    decimal price,
    DateOnly ReleaseDate
);
