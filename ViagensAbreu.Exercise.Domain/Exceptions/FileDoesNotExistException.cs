namespace ViagensAbreu.Exercise.Domain.Exceptions;

public class FileDoesNotExistException: Exception
{
    const string MESSAGE = "The file does not exist.";
    
    public FileDoesNotExistException(): base(MESSAGE) { }
}