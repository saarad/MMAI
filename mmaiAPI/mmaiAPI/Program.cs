using mmaiAPI.Extensions;
using graphQLService.Assemblers;
using mmaiAPI.Middlewares;

namespace mmaiAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddGraphQLServices();
            builder.Services.AddServices();
            builder.Services.AddRepositories(builder.Configuration);
            builder.Services.AddHttpClients(builder.Configuration);
            builder.Services.AddMmmaiCorsPolicy(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(MappingsProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline (middlewares will be invoked in registered order).
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHttpsRedirection();

            app.UseCors("_mmaiAllowedOriginsPolicy");

            app.UseAuthorization();

            app.MapControllers();
            app.MapGraphQL();

            app.Run();
        }
    }
}