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
}
