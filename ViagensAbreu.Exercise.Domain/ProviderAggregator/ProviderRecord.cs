namespace ViagensAbreu.Exercise.Domain.ProviderAggregator;

public record ProviderRecord(
    string Id, // IdRegisto
    DateTime BeginDate, // DataInicio
    DateTime EndDate, // DataTermo
    decimal SalePrice // ValorVendaProduto
    );
    
