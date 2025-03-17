using FinanzautoShool.Domain.Entity;

namespace FinanzautoShool.Infrastructure.Repositories.StudentRepositories.Interface;

public interface IStudentRepository
{
    Task<Student> CreateStudent(Student student);
    Task<IEnumerable<Student>> GetAllStudent();
    Task<Student> GetStudentById(int id);
    Task<bool> DeleteStudent(int id);
    Task<bool> UpdateStudent(Student student);
}