using MCPPractical.Models;
using MCPPractical.Services;

namespace MCPPractical.Endpoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/students")
            .WithTags("Students");

        // GET all students
        group.MapGet("/", (IStudentService service) =>
        {
            return Results.Ok(service.GetAll());
        })
        .WithName("GetAllStudents");

        // GET student by ID
        group.MapGet("/{id}", (int id, IStudentService service) =>
        {
            var student = service.GetById(id);
            if (student != null)
            {
                return Results.Ok(student);
            }
            return Results.NotFound($"Student with ID {id} not found");
        })
        .WithName("GetStudentById");

        // POST create new student
        group.MapPost("/", (Student student, IStudentService service) =>
        {
            if (!service.Add(student))
            {
                return Results.Conflict($"Student with ID {student.Id} already exists");
            }

            return Results.Created($"/students/{student.Id}", student);
        })
        .WithName("CreateStudent");

        // PUT update existing student
        group.MapPut("/{id}", (int id, Student updatedStudent, IStudentService service) =>
        {
            if (!service.Update(id, updatedStudent))
            {
                return Results.NotFound($"Student with ID {id} not found");
            }

            return Results.Ok(service.GetById(id));
        })
        .WithName("UpdateStudent");

        // DELETE student
        group.MapDelete("/{id}", (int id, IStudentService service) =>
        {
            if (!service.Delete(id))
            {
                return Results.NotFound($"Student with ID {id} not found");
            }

            return Results.NoContent();
        })
        .WithName("DeleteStudent");

        // GET student average score
        group.MapGet("/{id}/average", (int id, IStudentService service) =>
        {
            var student = service.GetById(id);
            if (student != null)
            {
                var average = service.CalculateAverage(student);
                return Results.Ok(new { StudentId = id, Name = student.Name, AverageScore = average });
            }
            return Results.NotFound($"Student with ID {id} not found");
        })
        .WithName("GetStudentAverage");
    }
}
