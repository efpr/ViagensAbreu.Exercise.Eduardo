using ViagensAbreu.Exercise.Domain.Exceptions;
using ViagensAbreu.Exercise.Domain.InsuranceAggregator;
using ViagensAbreu.Exercise.Domain.LocalAggregator;
using ViagensAbreu.Exercise.Domain.ProviderAggregator;
using ViagensAbreu.Exercise.Shared;

namespace ViagensAbreu.Exercise.Application.Services;

public class InsuranceService : IInsuranceService
{
    private readonly IProviderRepository? _providerRepository;
    private readonly ILocalRepository? _localRepository;

    public InsuranceService(){}
    public InsuranceService(IProviderRepository? providerRepository, ILocalRepository? localRepository)
    {
        _providerRepository = providerRepository;
        _localRepository = localRepository;
    }


    public async Task<Result<SyncSummary>> SyncInsurancesFromFiles(string providerPath, string localPath)
    {
        if (_providerRepository == null || _localRepository == null)
        {
            return new DependenciesException();
        }
        
        var providersListTask = _providerRepository.GetRecords(providerPath);
        var localListTask = _localRepository.GetRecords(localPath);
        
        var providerList = await providersListTask;
        var localList = await localListTask;

        if (!providerList.IsSuccessful || !localList.IsSuccessful)
        {
            return providerList.Exception ?? localList.Exception;
        }
        
        var syncResult = this.SyncInsurances(providerList.Value, localList.Value);
        await _providerRepository.SaveRecords(providerPath, syncResult.Providers);
        
        return syncResult.Status;
    }
    
    public InsuranceSyncSummary SyncInsurances(IList<ProviderRecord> providerList, IList<LocalRecord> localList)
    {
        var providerMap = providerList.GroupBy(x => x.Id)
            .ToDictionary(g => g.Key, g => g.ToList());
        var syncResult = new SyncSummary();
        var syncedProviders = new List<ProviderRecord>();
        int founded = 0;
        
        foreach (var local in localList)
        {
            if (providerMap.TryGetValue(local.ProviderName, out var providers))
            {
                foreach (var provider in providers)
                {
                    syncedProviders.Add(provider);
                    syncResult.Unchanged++;
                    founded++;
                }
                continue;
            }
            
            syncResult.Added++;
            syncedProviders.Add(new ProviderRecord(local.ProviderName, local.BeginDate, local.EndDate, local.Price));
        }
        
        
        syncResult.Removed = providerList.Count - founded;
        
        return new InsuranceSyncSummary(syncedProviders, syncResult);
    }
}



