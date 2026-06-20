using LMSPro.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LMSPro.Api.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case NotFoundException ex:
                context.Result = new NotFoundObjectResult(
                    new
                    {
                        Message = ex.Message
                    })
                {
                    StatusCode = 404
                };
                break;

            case ValidationException ex:
                context.Result = new ObjectResult(new
                {
                    Message = ex.Message,
                    Errors = ex.Errors
                })
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity
                };
                break;

            case UnauthorizedAccessException ex:
                context.Result = new UnauthorizedObjectResult(
                    new
                    {
                        Message = ex.Message
                    });
                break;

            default:
                context.Result = new ObjectResult(
                    new
                    {
                        Message = "Internal Server Error"
                    })
                {
                    StatusCode = 500
                };
                break;
        }

        context.ExceptionHandled = true;
    }

}
