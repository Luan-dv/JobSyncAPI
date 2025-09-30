using System.Net;

namespace JobSync.Exception.ExceptionBase;
public class ErrorOnValidationExceptions : JobSyncException
{
    private readonly List<string> _errors;
    public override int StatusCode => (int)HttpStatusCode.BadRequest;
    public ErrorOnValidationExceptions(List<string> errorMessages) : base(string.Empty)
    {
        _errors = errorMessages;
    }
    public override List<string> GetErrors()
    {
        return _errors;
    }
}
