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
        group.MapGet("/", (StudentStorage storage) =>
        {
            return Results.Ok(storage.GetAll());
        })
        .WithName("GetAllStudents");

        // GET student by ID
        group.MapGet("/{id}", (int id, StudentStorage storage) =>
        {
            var student = storage.GetById(id);
            if (student != null)
            {
                return Results.Ok(student);
            }
            return Results.NotFound($"Student with ID {id} not found");
        })
        .WithName("GetStudentById");

        // POST create new student
        group.MapPost("/", (Student student, StudentStorage storage) =>
        {
            if (!storage.Add(student))
            {
                return Results.Conflict($"Student with ID {student.Id} already exists");
            }

            return Results.Created($"/students/{student.Id}", student);
        })
        .WithName("CreateStudent");

        // PUT update existing student
        group.MapPut("/{id}", (int id, Student updatedStudent, StudentStorage storage) =>
        {
            if (!storage.Update(id, updatedStudent))
            {
                return Results.NotFound($"Student with ID {id} not found");
            }

            return Results.Ok(storage.GetById(id));
        })
        .WithName("UpdateStudent");

        // DELETE student
        group.MapDelete("/{id}", (int id, StudentStorage storage) =>
        {
            if (!storage.Delete(id))
            {
                return Results.NotFound($"Student with ID {id} not found");
            }

            return Results.NoContent();
        })
        .WithName("DeleteStudent");

        // GET student average score
        group.MapGet("/{id}/average", (int id, StudentStorage storage) =>
        {
            var student = storage.GetById(id);
            if (student != null)
            {
                var average = (student.Math + student.Science + student.English) / 3.0;
                return Results.Ok(new { StudentId = id, Name = student.Name, AverageScore = average });
            }
            return Results.NotFound($"Student with ID {id} not found");
        })
        .WithName("GetStudentAverage");
    }
}
