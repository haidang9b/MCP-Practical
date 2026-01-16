using MCPPractical.Models;
using MCPPractical.Services;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace MCPPractical.Tools;


[McpServerToolType]
public class StudentMcpTools
{
    private readonly StudentStorage _storage;

    public StudentMcpTools(StudentStorage storage)
    {
        _storage = storage;
    }

    [McpServerTool, Description("Get all students from the student list.")]
    public Task<List<Student>> GetStudentsAsync()
    {
        return Task.FromResult(_storage.GetAll().ToList());
    }

    [McpServerTool, Description("Calculate the average score of a student")]
    public Task<double> CalculateAverageAsync(
        [Description("Fiter by student name: return 0.0 if not found, else return the average score")]
        string name)
    {
        var students = _storage.GetAll();

        var student = students.FirstOrDefault(x => x.Name == name);

        if (student is null)
            return Task.FromResult(0.0);

        var average = (student.Math + student.Science + student.English) / 3.0;

        return Task.FromResult(average);
    }

    [McpServerTool, Description("Get the best student from the student list")]
    public Task<Student?> GetBestStudentAsync()
    {
        var students = _storage.GetAll();

        var best = students.OrderByDescending(x => (x.Math + x.Science + x.English) / 3.0).FirstOrDefault();

        return Task.FromResult(best);
    }

    [McpServerTool, Description("Add a new student to the list")]
    public Task<string> AddStudentAsync(
        [Description("Student ID")] int id,
        [Description("Student Name")] string name,
        [Description("Math Score")] int math,
        [Description("Science Score")] int science,
        [Description("English Score")] int english)
    {
        var student = new Student(id, name, math, science, english);
        if (_storage.Add(student))
        {
            return Task.FromResult($"Student {name} added successfully.");
        }
        return Task.FromResult($"Failed to add student. ID {id} might already exist.");
    }

    [McpServerTool, Description("Update an existing student's information")]
    public Task<string> UpdateStudentAsync(
         [Description("Student ID to update")] int id,
         [Description("New Student Name")] string name,
         [Description("New Math Score")] int math,
         [Description("New Science Score")] int science,
         [Description("New English Score")] int english)
    {
        var student = new Student(id, name, math, science, english);
        if (_storage.Update(id, student))
        {
            return Task.FromResult($"Student {id} updated successfully.");
        }
        return Task.FromResult($"Failed to update student. ID {id} not found.");
    }
}
