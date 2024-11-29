
using ViagensAbreu.Exercise.Shared;

namespace ViagensAbreu.Exercise.Domain.LocalAggregator;

public interface ILocalRepository
{
    Task<Result<IList<LocalRecord>>> GetRecords(string path);
}