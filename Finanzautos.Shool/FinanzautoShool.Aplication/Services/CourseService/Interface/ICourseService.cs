using FinanzautoShool.Domain.Dto;

namespace FinanzautoShool.Aplication.Services.CourseService.Interface;

public interface ICourseService
{
    Task<CourseDto> CreateCourse(CreateCourseDto createCourseDto);
    Task<IEnumerable<CourseDto>> GetAllCourse();
    Task<CourseDto> GetCourseById(int id);
    Task<DeleteResult> DeleteCourse(int id);
    Task<int?> UpdateCourse(int id, UpdateCourseDto updateCourseDto);
}