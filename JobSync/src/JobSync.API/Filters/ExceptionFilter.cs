using JobSync.Communication.Responses;
using JobSync.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.InteropServices;

namespace JobSync.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is JobSyncException)
        {
            HandlerProejectException();

        }
        else
        {

        }
    }

    private void HandlerProejectException(ExceptionContext context)
    {
        var jobSyncException = (JobSyncException)context.Exception; //casting
        var errorResponse = new ResponseErrorJson(jobSyncException.GetErrors());
    }






}

