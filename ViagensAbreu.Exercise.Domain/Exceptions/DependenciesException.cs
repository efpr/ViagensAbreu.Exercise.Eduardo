namespace ViagensAbreu.Exercise.Domain.Exceptions;

public class DependenciesException: Exception
{
    const string MESSAGE = "There was an error with the dependencies of the application.";
    
    public DependenciesException(): base(MESSAGE) { }
}