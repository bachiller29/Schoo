using FinanzautoShool.Domain.Entity;
using FinanzautoShool.Infrastructure.Repositories.TeacherRepositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinanzautoShool.Infrastructure.Repositories.TeacherRepositories.Repository
{
    public class TeacherRepository :ITeacherRepository
    {
        private readonly SchoolDbContext _context;

        public TeacherRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<Teacher> CreateTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<bool> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return false;

            _context.Teachers.Remove(teacher);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeacher()
        {
            return await _context.Teachers.AsNoTracking().ToListAsync();
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            return await _context.Teachers.FindAsync(id);
        }

        public async Task<bool> UpdateTeacher(Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentException(nameof(teacher));

            var existingTeacher = await _context.Teachers.FindAsync(teacher.Id);
            if (existingTeacher == null) return false;

            _context.Entry(existingTeacher).CurrentValues.SetValues(teacher);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
