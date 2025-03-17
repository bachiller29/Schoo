using FinanzautoShool.Domain.Entity;
using FinanzautoShool.Infrastructure.Repositories.QualificationRepositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinanzautoShool.Infrastructure.Repositories.QualificationRepositories.Repository
{
    public class QualificationRepository : IQualificationRepository
    {
        private readonly SchoolDbContext _context;

        public QualificationRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<Qualification> CreateQualification(Qualification qualification)
        {
            _context.Qualifications.Add(qualification);
            await _context.SaveChangesAsync();
            return qualification;
        }

        public async Task<bool> DeleteQualification(int id)
        {
            var qualification= await _context.Qualifications.FindAsync(id);
            if (qualification == null) return false;

            _context.Qualifications.Remove(qualification);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Qualification>> GetAllQualification()
        {
            return await _context.Qualifications.AsNoTracking().ToListAsync();
        }

        public async Task<Qualification> GetQualificationById(int id)
        {
            return await _context.Qualifications.FindAsync(id);
        }

        public async Task<bool> UpdateQualification(Qualification qualification)
        {
            if (qualification == null)
                throw new ArgumentException(nameof(qualification));

            var existingQualification = await _context.Qualifications.FindAsync(qualification.Id);
            if (existingQualification == null) return false;

            _context.Entry(existingQualification).CurrentValues.SetValues(qualification);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
