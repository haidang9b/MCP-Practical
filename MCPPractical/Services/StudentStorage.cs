using System.Collections.Concurrent;
using MCPPractical.Models;

namespace MCPPractical.Services;

/// <summary>
/// Singleton service to manage student data storage using a thread-safe ConcurrentDictionary
/// </summary>
public class StudentStorage
{
    private readonly ConcurrentDictionary<int, Student> _students = new();

    public StudentStorage()
    {
        // Seed initial data
        _students.TryAdd(1, new Student(1, "John Doe", 85, 90, 88));
        _students.TryAdd(2, new Student(2, "Jane Smith", 92, 87, 95));
        _students.TryAdd(3, new Student(3, "Bob Johnson", 78, 82, 80));
    }

    /// <summary>
    /// Get all students
    /// </summary>
    public IEnumerable<Student> GetAll()
    {
        return _students.Values.ToList();
    }

    /// <summary>
    /// Get a student by ID
    /// </summary>
    public Student? GetById(int id)
    {
        return _students.TryGetValue(id, out var student) ? student : null;
    }

    /// <summary>
    /// Add a new student
    /// </summary>
    public bool Add(Student student)
    {
        return _students.TryAdd(student.Id, student);
    }

    /// <summary>
    /// Update an existing student
    /// </summary>
    public bool Update(int id, Student student)
    {
        if (!_students.ContainsKey(id))
        {
            return false;
        }
        
        _students[id] = student with { Id = id };
        return true;
    }

    /// <summary>
    /// Delete a student by ID
    /// </summary>
    public bool Delete(int id)
    {
        return _students.TryRemove(id, out _);
    }

    /// <summary>
    /// Check if a student exists
    /// </summary>
    public bool Exists(int id)
    {
        return _students.ContainsKey(id);
    }
}
