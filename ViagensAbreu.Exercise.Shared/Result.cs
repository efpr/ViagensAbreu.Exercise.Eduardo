namespace ViagensAbreu.Exercise.Shared;

public class Result<T>
{
    public T Value { get; }
    public Exception? Exception { get; }
    public bool IsSuccessful { get; }
    
    public Result(T value)
    {
        Value = value;
        IsSuccessful = true;
        Exception = null;
    }
    
    public Result(Exception exception)
    {
        Exception = exception;
        IsSuccessful = false;
    }
    
    public static implicit operator Result<T>(T value) => new Result<T>(value);
    public static implicit operator Result<T>(Exception exception) => new Result<T>(exception);
    
}