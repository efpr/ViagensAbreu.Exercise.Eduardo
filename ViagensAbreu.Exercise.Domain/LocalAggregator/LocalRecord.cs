namespace ViagensAbreu.Exercise.Domain.LocalAggregator;

public record LocalRecord(
    int Id, 
    string ProviderName,
    decimal Price,
    DateTime BeginDate,
    DateTime EndDate
    );