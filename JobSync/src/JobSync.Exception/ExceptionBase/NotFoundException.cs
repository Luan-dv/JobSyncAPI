using System.Net;

namespace JobSync.Exception.ExceptionBase;
public class NotFoundException : JobSyncException
{
    public NotFoundException(string message) : base(message)
    {
        
    }
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return new List<string> { Message };
    }   
}
