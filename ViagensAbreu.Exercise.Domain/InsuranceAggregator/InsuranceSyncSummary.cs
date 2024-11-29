using ViagensAbreu.Exercise.Domain.ProviderAggregator;

namespace ViagensAbreu.Exercise.Domain.InsuranceAggregator;

public record InsuranceSyncSummary(
    IList<ProviderRecord> Providers,
    SyncSummary Status
);