namespace GameStore.api.Dtos;

public record class GameSummaryDto(
    int Id,
    string Name,
    String Genre,
    decimal price,
    DateOnly ReleaseDate
);
