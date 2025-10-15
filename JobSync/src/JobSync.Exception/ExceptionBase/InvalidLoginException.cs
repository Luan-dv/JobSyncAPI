using System.Net;

namespace JobSync.Exception.ExceptionBase;
public class InvalidLoginException : JobSyncException
{
    public InvalidLoginException() : base(ResourceErrorMessages.INVALID_PASSWORD_OR_EMAIL) //trocar depois
    {

    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;
    public override List<string> GetErrors()
    {
        return [Message];
    }
}
