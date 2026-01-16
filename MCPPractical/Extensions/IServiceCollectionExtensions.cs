using MCPPractical.Services;
using MCPPractical.Storages;
using Microsoft.OpenApi;
using Refit;

namespace MCPPractical.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDocumentApi();
        services.AddCustomCors();
        services.AddDependencies();

        services.AddMcpServer(o =>
            {
                o.ServerInfo = new()
                {
                    Title = "MCP Server",
                    Name = "MCP Server",
                    Version = "0.0.1"
                };
            })
            .WithHttpTransport()
            .WithToolsFromAssembly()
            .WithResourcesFromAssembly()
            .WithPromptsFromAssembly();


        services.AddRefitClient<IExchangeRateApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration.GetSection("ExchangeRateUrl").Value ?? throw new InvalidOperationException());
            });

        return services;
    }

    private static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddSingleton<StudentStorage>();
        services.AddSingleton<ExchangeRateStorage>();

        services.AddSingleton<IStudentService, StudentService>();

        return services;
    }

    private static IServiceCollection AddDocumentApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Student Score Management API",
                Version = "v1",
                Description = "A minimal API for managing student scores with Math, Science, and English subjects"
            });
        });

        return services;
    }

    private static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
         {
             options.AddDefaultPolicy(policy =>
             {
                 policy
                     .AllowAnyOrigin()
                     .AllowAnyMethod()
                     .WithHeaders(
                         "Content-Type",
                         "Mcp-Session-Id"
                      )
                     .WithExposedHeaders(
                         "Mcp-Session-Id"
                     );
             });
         });

        return services;
    }
}
