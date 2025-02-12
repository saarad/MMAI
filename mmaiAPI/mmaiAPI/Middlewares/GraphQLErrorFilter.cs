using graphQLService.Exceptions;
using HotChocolate.AspNetCore.Serialization;
using HotChocolate.Execution;
using System.Net;

namespace mmaiAPI.Middlewares
{
    /// <summary>
    /// If any error occurs in any module used in the given request (e.g. FighterService, MatchhistoryService etc.), it will be catched and handled here.
    /// </summary>
    public class GraphQLErrorFilter : IErrorFilter
    {

        public IError OnError(IError error)
        {
            var exception = error.Exception ?? new Exception();
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Internal error. ";

            switch (exception)
            {
                case MLEngineException:
                    // MLEngine error
                    message += "Call to machine learning engine failed. ";
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                case NotFoundException:
                    // not found error
                    statusCode = (int)HttpStatusCode.NotFound;
                    message = "Not found. ";
                    break;
                default:
                    // unhandled error
                    break;
            }

            message += $"Error: {exception.Message}";

            //todo: Add logging and reporting to mmai e-mail address. 

            return error
                .WithCode(statusCode.ToString())
                .WithMessage(message)
                .RemovePath() //removing stacktrace to avoid exposing code and paths to client
                .RemoveException()
                .RemoveExtensions();
        }
    }
}
