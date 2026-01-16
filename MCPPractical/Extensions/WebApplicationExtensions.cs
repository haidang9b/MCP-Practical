namespace MCPPractical.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseApplication(this WebApplication app)
    {
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

        return app;
    }
}
