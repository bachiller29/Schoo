using FinanzautoShool.Domain.Dto;

namespace FinanzautoShool.Aplication.Services.QualificationService.Interface;

public interface IQualificationService
{
    Task<QualificationDto> CreateQualification(CreateQualificationDto createCategoryDto);
    Task<IEnumerable<QualificationDto>> GetAllQualification();
    Task<QualificationDto> GetQualificationById(int id);
    Task<DeleteResult> DeleteQualification(int id);
    Task<int?> UpdateQualification(int id, UpdateQualificationDto updateStudentDto);
}