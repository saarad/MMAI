using HotChocolate.AspNetCore.Serialization;
using HotChocolate.Execution;
using System.Net;


namespace mmaiAPI.Middlewares
{
    /// <summary>
    /// Overwriting default 500 status code on error with HttpResultSerializer to more describing codes (404, 401 etc)
    /// </summary>
    public class GraphQLHttpResultSerializer : DefaultHttpResultSerializer
    {
        public override HttpStatusCode GetStatusCode(IExecutionResult result)
        {
            var baseStatusCode = base.GetStatusCode(result);

            if (result is IQueryResult && baseStatusCode == HttpStatusCode.InternalServerError && result.Errors?.Count > 0)
            {
                var code = result.Errors.FirstOrDefault()?.Code ?? "500";
                var httpStatusCode = (HttpStatusCode)int.Parse(code); //variable only used for improved code readability

                return httpStatusCode;
            }

            return baseStatusCode;
        }
    }
}
