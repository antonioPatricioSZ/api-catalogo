using System.Net;
using APICatalogo.Communication.Responses;
using APICatalogo.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.API.Filters;

public class CustomExceptionsFilter : IExceptionFilter {

    public void OnException(ExceptionContext context) {

        if(context.Exception is APICatalogoException) {
            TratarAPICatalogoException(context);
        } else if(context.Exception is FluentValidation.ValidationException) {
            TratarFluentValidationException(context);
        } else {
            LancarErroDesconhecido(context);
        }
        
    }


    private static void TratarFluentValidationException(ExceptionContext context) {
        var fluentValidationException = context.Exception as FluentValidation.ValidationException;
        
        var errors = fluentValidationException?.Errors
                .Select(
                    error => $"{error.PropertyName}: {error.ErrorMessage}"
                ).ToList();

        var result = new ObjectResult(
            new ErrorReponseJson(errors)
        ){
            StatusCode = (int)HttpStatusCode.BadRequest,
        };

        context.Result = result;
        context.ExceptionHandled = true;
    }


    private static void TratarAPICatalogoException(ExceptionContext context) {

        if (context.Exception is NotFoundException notFoundException){
            
            context.Result = new NotFoundObjectResult(
                new ErrorReponseJson(notFoundException.Message)
            );

        } else if (context.Exception is InvalidLoginException invalidLoginException) {
            
            context.Result = new UnauthorizedObjectResult(
                new ErrorReponseJson(invalidLoginException.Message)
            );

        } else if(context.Exception is ForbidenException forbidenException) {
            
            context.Result = new ObjectResult(
                new ErrorReponseJson(forbidenException.Message)
            ){
                StatusCode = (int)HttpStatusCode.Forbidden
            };

        } else if (context.Exception is ValidationErrorException validationErrorException) {

            context.Result = new BadRequestObjectResult(
                new ErrorReponseJson(validationErrorException!.ErrorMessages)
            );

        }
    }


    private static void LancarErroDesconhecido(ExceptionContext context) {
        context.Result = new ObjectResult(
                new ErrorReponseJson("Erro desconhecido.")
        ){
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
    }

}
