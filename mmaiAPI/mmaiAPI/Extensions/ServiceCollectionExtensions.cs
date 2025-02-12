using dataAccess.Interfaces;
using dataAccess;
using graphQLService.Services.Interfaces.DependencyInjection;
using graphQLService.HttpClients.MLEngine;

namespace mmaiAPI.Extensions
{
    /// <summary>
    /// Using Scrutor for automatic Dependency injection
    /// Every interface and/or class that implements IRepository will be registrered as a Transient service
    /// Every interface and/or class that implements ITransient will be registrered as a Transient service
    /// Every interface and/or class that implements IScoped will be registrered as a Scoped service
    /// Every interface and/or class that implements ISingleton will be registrered as a Singleton service
    /// Example for transient service: IFighterService : ITransientService -> FighterService : IFighterService 
    ///     -> IFighterService is now registered as transient service and can be injected
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            DbConnectionFactory dbFactory = new DbConnectionFactory(configuration.GetConnectionString("connection"), configuration.GetConnectionString("db"));
            services.AddSingleton<IDbConnectionFactory>(_ => dbFactory); //Recommended lifetime for MongoClient is Singleton

            services.Scan(scan => scan
            .FromAssemblyOf<IRepository>()
            .AddClasses(classes => classes.AssignableTo<IRepository>())
                                                 .AsImplementedInterfaces()
                                                 .WithTransientLifetime());

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddTransientServices()
                .AddScopedServices()
                .AddSingletonServices();
            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddMLEngineHttpClient(configuration);
        }

        public static IServiceCollection AddMmmaiCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedClientOrigin = configuration.GetSection("AllowedMmaiClientOrigin").Value;
            return services.AddCors(options =>
            {
                options.AddPolicy("_mmaiAllowedOriginsPolicy", builder =>
                {
                    builder.WithOrigins(allowedClientOrigin).AllowAnyMethod().AllowAnyHeader();
                });
            });
        }

        private static IServiceCollection AddMLEngineHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services
                  .AddHttpClient<MLEngineHttpClient>(c => c.BaseAddress = new Uri(configuration.GetSection("mlengineUrl").Value)); //can also add handler here if auth is added to mlengine
            return services;
        }

        private static IServiceCollection AddTransientServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
            .FromAssemblyOf<ITransient>()
            .AddClasses(classes => classes.AssignableTo<ITransient>())
                                                 .AsImplementedInterfaces()
                                                 .WithTransientLifetime());

            return services;
        }

        private static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
            .FromAssemblyOf<IScoped>()
            .AddClasses(classes => classes.AssignableTo<IScoped>())
                                                 .AsImplementedInterfaces()
                                                 .WithScopedLifetime());

            return services;
        }

        private static IServiceCollection AddSingletonServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
            .FromAssemblyOf<ISingleton>()
            .AddClasses(classes => classes.AssignableTo<ISingleton>())
                                                 .AsImplementedInterfaces()
                                                 .WithSingletonLifetime());

            return services;
        }
    }
}
