using System.Globalization;
using ViagensAbreu.Exercise.Domain.ProviderAggregator;
using ViagensAbreu.Exercise.Shared;

namespace ViagensAbreu.Exercise.Infra;

public class ProviderRepository : IProviderRepository
{
    public async Task<Result<IList<ProviderRecord>>> GetRecords(string path)
    {
        var records = await FileUtils.ReadFile<ProviderRecord>(path, x =>
        {
            var parts = x.Split(';');
            return new ProviderRecord(
                parts[0], 
                Convert.ToDateTime(parts[1]),
                Convert.ToDateTime(parts[2]),
                decimal.Parse(parts[3], new CultureInfo("en-US"))
                );
        });
        
        return records;
    }

    public async Task SaveRecords(string path, IList<ProviderRecord> records)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        
        await FileUtils.WriteFile(path, records,
            x => $"{x.Id};{x.BeginDate};{x.EndDate};{x.SalePrice}");
    }
}