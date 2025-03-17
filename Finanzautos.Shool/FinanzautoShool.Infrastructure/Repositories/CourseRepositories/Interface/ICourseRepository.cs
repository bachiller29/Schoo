using FinanzautoShool.Domain.Entity;

namespace FinanzautoShool.Infrastructure.Repositories.CourseRepositories.Interface;

public interface ICourseRepository
{
    Task<Course> CreateCourse(Course  course);
    Task<IEnumerable<Course>> GetAllCourse();
    Task<Course> GetCourseById(int id);
    Task<bool> DeleteCourse(int id);
    Task<bool> UpdateCourse(Course course);
}