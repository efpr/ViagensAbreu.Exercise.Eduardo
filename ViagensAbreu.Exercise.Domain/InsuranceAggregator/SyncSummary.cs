namespace ViagensAbreu.Exercise.Domain.InsuranceAggregator;

public class SyncSummary
{
    public int Unchanged { get; set; } = 0;
    public int Added { get; set; } = 0;
    public int Removed { get; set; } = 0;
}