using MCPPractical.Endpoints;
using MCPPractical.Services;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Student Score Management API",
        Version = "v1",
        Description = "A minimal API for managing student scores with Math, Science, and English subjects"
    });
});

builder.Services.AddCors(options =>
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

builder.Services
    .AddMcpServer(o =>
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

// Register StudentStorage as a singleton
builder.Services.AddSingleton<StudentStorage>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Student API v1");
        options.RoutePrefix = "swagger"; // Access Swagger UI at /swagger
    });
}

app.UseHttpsRedirection();

// Map student endpoints
app.MapStudentEndpoints();

app.MapMcp("/mcp");

app.Run();
