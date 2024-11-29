
using ViagensAbreu.Exercise.Domain.LocalAggregator;
using ViagensAbreu.Exercise.Domain.ProviderAggregator;
using ViagensAbreu.Exercise.Shared;

namespace ViagensAbreu.Exercise.Domain.InsuranceAggregator;

public interface IInsuranceService
{
        Task<Result<SyncSummary>> SyncInsurancesFromFiles(string providerPath, string localPath);
        InsuranceSyncSummary SyncInsurances(IList<ProviderRecord> providerList, IList<LocalRecord> localList);
}