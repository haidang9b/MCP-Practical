namespace MCPPractical.Services;

using MCPPractical.Models;

public interface IStudentService
{
    IEnumerable<Student> GetAll();
    Student? GetById(int id);
    bool Add(Student student);
    bool Update(int id, Student student);
    bool Delete(int id);
    double CalculateAverage(Student student);
    Student? GetBestStudent();
}
