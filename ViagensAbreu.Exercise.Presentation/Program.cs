using ViagensAbreu.Exercise.Application.Services;
using ViagensAbreu.Exercise.Domain.LocalAggregator;
using ViagensAbreu.Exercise.Domain.ProviderAggregator;
using ViagensAbreu.Exercise.Infra;

IProviderRepository providerRepository = new ProviderRepository();
ILocalRepository localRepository = new LocalRepository();

var insuranceService = new InsuranceService(providerRepository, localRepository);

if (args.Length != 2)
{
    Console.WriteLine("São necessários dois argumentos: o caminho para o ficheiro provider e o caminho para o ficheiro local");
    return;
}

var result = await insuranceService.SyncInsurancesFromFiles(args[0], args[1]);

if (result.IsSuccessful)
{
    Console.WriteLine("{0} registos Ok", result.Value.Unchanged);
    Console.WriteLine("{0} registos removidos", result.Value.Removed);
    Console.WriteLine("{0} registos criados", result.Value.Removed);
}
else
{
    Console.WriteLine($"Failed: {result.Exception?.Message}");
}