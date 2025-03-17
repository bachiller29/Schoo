using FinanzautoShool.Domain.Entity;

namespace FinanzautoShool.Infrastructure.Repositories.TeacherRepositories.Interface;

public interface ITeacherRepository
{
    Task<Teacher> CreateTeacher(Teacher teacher);
    Task<IEnumerable<Teacher>> GetAllTeacher();
    Task<Teacher> GetTeacherById(int id);
    Task<bool> DeleteTeacher(int id);
    Task<bool> UpdateTeacher(Teacher teacher);
}