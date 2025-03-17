using FinanzautoShool.Domain.Dto;

namespace FinanzautoShool.Aplication.Services.StudentService.Interface;

public interface IStudentService
{
    Task<StudentDto> CreateStudent(CreateStudentDto createStudentDto);
    Task<IEnumerable<StudentDto>> GetAllStudent();
    Task<StudentDto> GetStudentById(int id);
    Task<DeleteResult> DeleteStudent(int id);
    Task<int?> UpdateStudent(int id, UpdateStudentDto updateStudentDto);
}