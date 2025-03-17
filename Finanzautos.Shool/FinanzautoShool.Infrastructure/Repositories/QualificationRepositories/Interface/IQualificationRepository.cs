using FinanzautoShool.Domain.Entity;

namespace FinanzautoShool.Infrastructure.Repositories.QualificationRepositories.Interface;

public interface IQualificationRepository
{
    Task<Qualification> CreateQualification(Qualification qualification);
    Task<IEnumerable<Qualification>> GetAllQualification();
    Task<Qualification> GetQualificationById(int id);
    Task<bool> DeleteQualification(int id);
    Task<bool> UpdateQualification(Qualification qualification);
}