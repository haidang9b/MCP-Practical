using MCPPractical.Models;
using MCPPractical.Storages;

namespace MCPPractical.Services;

public class StudentService : IStudentService
{
    private readonly StudentStorage _storage;

    public StudentService(StudentStorage storage)
    {
        _storage = storage;
    }

    public IEnumerable<Student> GetAll()
    {
        return _storage.GetAll();
    }

    public Student? GetById(int id)
    {
        return _storage.GetById(id);
    }

    public bool Add(Student student)
    {
        return _storage.Add(student);
    }

    public bool Update(int id, Student student)
    {
        return _storage.Update(id, student);
    }

    public bool Delete(int id)
    {
        return _storage.Delete(id);
    }

    public double CalculateAverage(Student student)
    {
        return (student.Math + student.Science + student.English) / 3.0;
    }

    public Student? GetBestStudent()
    {
        return _storage.GetAll()
            .OrderByDescending(x => CalculateAverage(x))
            .FirstOrDefault();
    }
}
