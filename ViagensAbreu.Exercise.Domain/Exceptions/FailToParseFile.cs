namespace ViagensAbreu.Exercise.Domain.Exceptions;

public class FailToParseFile: Exception
{
    const string MESSAGE = "There was an error while parsing the file.";
    
    public FailToParseFile(): base(MESSAGE) { }
}