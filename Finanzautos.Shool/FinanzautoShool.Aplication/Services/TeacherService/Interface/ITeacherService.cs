using FinanzautoShool.Domain.Dto;

namespace FinanzautoShool.Aplication.Services.TeacherService.Interface;

public interface ITeacherService
{
    Task<TeacherDto> CreateTeacher(CreateTeacherDto createCategoryDto);
    Task<IEnumerable<TeacherDto>> GetAllTeacher();
    Task<TeacherDto> GetTeacherById(int id);
    Task<DeleteResult> DeleteTeacher(int id);
    Task<int?> UpdateTeacher(int id, UpdateTeacherDto updateTeacherDto);
}