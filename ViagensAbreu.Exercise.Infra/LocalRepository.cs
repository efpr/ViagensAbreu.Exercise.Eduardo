using System.Globalization;
using ViagensAbreu.Exercise.Domain.LocalAggregator;
using ViagensAbreu.Exercise.Shared;

namespace ViagensAbreu.Exercise.Infra;

public class LocalRepository : ILocalRepository
{
    public async Task<Result<IList<LocalRecord>>> GetRecords(string path)
    {
        var records = await FileUtils.ReadFile<LocalRecord>(path, x =>
        {
            var parts = x.Split(';');
            return new LocalRecord(
                int.Parse(parts[0]), 
                parts[1],
                decimal.Parse(parts[2], new CultureInfo("en-US")),
                Convert.ToDateTime(parts[3]),
                Convert.ToDateTime(parts[4])
                );
        });

        return records;
    }
}