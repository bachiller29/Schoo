using FinanzautoShool.Domain.Entity;
using FinanzautoShool.Infrastructure.Repositories.CourseRepositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinanzautoShool.Infrastructure.Repositories.CourseRepositories.Repository
{
    internal class CourseRepository : ICourseRepository
    {
        private readonly SchoolDbContext _context;

        public CourseRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<Course> CreateCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Course>> GetAllCourse()
        {
            return await _context.Courses.AsNoTracking().ToListAsync();
        }

        public async Task<Course> GetCourseById(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<bool> UpdateCourse(Course course)
        {
            if (course == null)
                throw new ArgumentException(nameof(course));

            var existingCourse = await _context.Courses.FindAsync(course.Id);
            if (existingCourse == null) return false;
            
            _context.Entry(existingCourse).CurrentValues.SetValues(course);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
