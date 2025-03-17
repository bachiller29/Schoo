using FinanzautoShool.Aplication.Services.StudentService.Interface;
using FinanzautoShool.Domain.Dto;
using FinanzautoShool.Domain.Entity;
using FinanzautoShool.Infrastructure.Repositories.StudentRepositories.Interface;

namespace FinanzautoShool.Aplication.Services.StudentService.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<StudentDto> CreateStudent(CreateStudentDto createStudentDto)
            {
            var student = new Student
            {
                FirstName = createStudentDto.FirstName,
                LastName = createStudentDto.LastName,
                BirthDate = createStudentDto.BirthDate,
                Email = createStudentDto.Email,
                CreatedAt = DateTime.Now,
            };

            var createStudent = await _repository.CreateStudent(student);

            return new StudentDto
            {
                Id = createStudent.Id,
                FirstName = createStudent.FirstName,
                LastName = createStudent.LastName,
                BirthDate = createStudent.BirthDate,
                Email = createStudent.Email,
            };
        }

        public async Task<DeleteResult> DeleteStudent(int id)
        {
            var student = await _repository.GetStudentById(id);

            if (student == null)
            {
                return new DeleteResult
                {
                    Message = "No se encontro un estudiante con el Id proporcionado.",
                    IsSuccess = false
                };
            }

            var isDelete = await _repository.DeleteStudent(id);

            if (!isDelete)
            {
                return new DeleteResult
                {
                    Message = "No se pudo eliminar el estudiante.",
                    IsSuccess = false
                };
            }

            return new DeleteResult()
            {
                Message = "El estudainte fue eliminado correctamente",
                IsSuccess = true
            };
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudent()
        {
            var student = await _repository.GetAllStudent();
            return student.Select(s => new StudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                BirthDate = s.BirthDate,
                Email = s.Email,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
            });
        }

        public async Task<StudentDto> GetStudentById(int id)
        {
            var student = await _repository.GetStudentById(id);
            if (student == null) return null;

            return new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                Email = student.Email,
                CreatedAt = student.CreatedAt,
                UpdatedAt = student.UpdatedAt,
            };
        }

        public async Task<int?> UpdateStudent(int id, UpdateStudentDto updateStudentDto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del estudainte es obligatorio y debe ser un numero valido.");

            var existingStudent = await _repository.GetStudentById(id);
            if (existingStudent == null) return null;

            if(updateStudentDto.FirstName != null)
                existingStudent.FirstName = updateStudentDto.FirstName;

            if (updateStudentDto.LastName != null)
                existingStudent.LastName = updateStudentDto.LastName;

            if (updateStudentDto.BirthDate.HasValue)
            {
                existingStudent.BirthDate = updateStudentDto.BirthDate.Value;
            }
            
            if (updateStudentDto.Email != null)
                existingStudent.Email = updateStudentDto.Email;

            existingStudent.UpdatedAt = DateTime.Now;

            var isUpdate = await _repository.UpdateStudent(existingStudent);
            if (!isUpdate) return null;

            return existingStudent.Id;
        }
    }
}
