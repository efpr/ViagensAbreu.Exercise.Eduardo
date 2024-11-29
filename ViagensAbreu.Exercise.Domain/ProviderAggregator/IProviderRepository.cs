using ViagensAbreu.Exercise.Shared;

namespace ViagensAbreu.Exercise.Domain.ProviderAggregator;

public interface IProviderRepository
{
    Task<Result<IList<ProviderRecord>>> GetRecords(string path);
    Task SaveRecords(string path, IList<ProviderRecord> records);
}