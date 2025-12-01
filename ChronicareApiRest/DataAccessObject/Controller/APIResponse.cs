namespace ChronicareApiRest.DataAccessObject.Controller;

public class APIResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public object? Result { get; set; }
    public List<string>? Errors { get; set; }

    public APIResponse(bool isSuccess = true, string? message = null, object? result = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Result = result;
        Errors = new List<string>();
    }
}
