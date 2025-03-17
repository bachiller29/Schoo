using FinanzautoShool.Domain.Entity;
using FinanzautoShool.Infrastructure.Repositories.StudentRepositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinanzautoShool.Infrastructure.Repositories.StudentRepositories.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolDbContext _context;

        public StudentRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<Student> CreateStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Student>> GetAllStudent()
        {
            return await _context.Students.AsNoTracking().ToListAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            if (student == null)
                throw new ArgumentException(nameof(student));

            var existingStudent = await _context.Students.FindAsync(student.Id);
            if (existingStudent == null) return false;

            _context.Entry(existingStudent).CurrentValues.SetValues(student);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
