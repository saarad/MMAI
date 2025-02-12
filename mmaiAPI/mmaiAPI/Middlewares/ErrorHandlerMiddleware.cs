using graphQLService.Exceptions;
using System.Net;
using System.Text.Json;

namespace mmaiAPI.Middlewares
{
    /// <summary>
    /// This middleware will try-catch all requests to the API.
    /// If any error occurs before the GraphQL Query/Mutation executes, it will be handled here.
    /// Note that errors that occur inside a Query/Mutation execution will not be handled here, but in GraphQLErrorFilter
    /// 
    /// This Middleware is added in order to handle errors that might occur outside the GraphQL Query/Mutation scope, 
    /// i.e. errors that occur before the Hotchocolate middleware for GraphQL executes. 
    /// Examples of this could be errors that occur when fetching schemas, configuration errors etc.
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var message = $"Internal server error. {error.Message}";
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //todo: Add logging and reporting to mmai e-mail address.
                var result = JsonSerializer.Serialize(new { message = message });
                await response.WriteAsync(result);
            }
        }
    }
}
