using ViagensAbreu.Exercise.Domain.Exceptions;
using ViagensAbreu.Exercise.Shared;

namespace ViagensAbreu.Exercise.Infra;

public static class FileUtils
{
    public static async Task<Result<IList<T>>> ReadFile<T>(string path, Func<string, T> parse)
    {
        if (!File.Exists(path))
        {
            return new FileDoesNotExistException();
        }
        
        var result = new List<T>();
        try
        {
            var lines = await File.ReadAllLinesAsync(path);
            foreach (var line in lines)
            {
                result.Add(parse(line));
            }
        }
        catch (Exception ex)
        {
            return new FailToParseFile();
        }
        
        return result;
    }
    
    public static async Task WriteFile<T>(string path, IList<T> data, Func<T, string> serialize)
    {
        var lines = data.Select(serialize);
        await File.WriteAllLinesAsync(path, lines);
    }
}