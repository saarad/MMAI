using graphQLService.Schema;
using mmaiAPI.Middlewares;

namespace mmaiAPI.Extensions
{
    public static class GraphQLExtensions
    {
        public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
        {
            //It is possible to add multiple query and mutation types if needed for readability (splitting methods into multiple files), i.e. fighterQueryType, matchHistoryQueryType etc.
            services.AddGraphQLServer()
                .AddQueryType<Query>() 
                .AddMutationType<Mutation>();

            services.AddErrorFilter<GraphQLErrorFilter>();
            services.AddHttpResultSerializer<GraphQLHttpResultSerializer>();

            return services;
        }
    }
}
