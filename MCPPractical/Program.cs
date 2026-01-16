using MCPPractical.Endpoints;
using MCPPractical.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.UseApplication();

app.MapStudentEndpoints();

app.MapMcp("/mcp");

app.Run();
