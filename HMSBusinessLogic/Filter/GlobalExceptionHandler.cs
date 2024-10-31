using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;

namespace HMSBusinessLogic.Filter
{
    public class GlobalExceptionHandler :ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var statusCode= StatusCodes.Status500InternalServerError;

            switch (context.Exception)
            {
                case NotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;

                case BadRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case ConflictException:
                    statusCode = (int)HttpStatusCode.Conflict;
                    break;

                case UnauthorizedException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = new ErrorResponse
            {
                message = context.Exception.Message
            };

            context.Result = new ObjectResult(result)
            {
                StatusCode = statusCode,
                DeclaredType = typeof(ErrorResponse),
            };
        }

        public class ErrorResponse
        {
            public string message { get; set; }
        }
    }
}
