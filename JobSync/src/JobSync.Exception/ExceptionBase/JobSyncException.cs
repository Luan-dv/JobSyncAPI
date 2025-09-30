namespace JobSync.Exception.ExceptionBase;
public abstract class JobSyncException : SystemException
{
    protected JobSyncException(string message) : base(message)
    {

    }
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
