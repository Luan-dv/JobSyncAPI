using JobSync.Communication.Responses;
using JobSync.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.InteropServices;

namespace JobSync.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is JobSyncException)
        {
            HandlerProejectException(context);

        }
        else
        {
            ThrowUnknownError(context);
        }
    }

    private void HandlerProejectException(ExceptionContext context)
    {
        var jobSyncException = (JobSyncException)context.Exception; //casting
        var errorResponse = new ResponseErrorJson(jobSyncException.GetErrors());

        context.HttpContext.Response.StatusCode = jobSyncException.StatusCode;

        context.Result = new ObjectResult(errorResponse);

    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson("Bateu aqui");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }




}

