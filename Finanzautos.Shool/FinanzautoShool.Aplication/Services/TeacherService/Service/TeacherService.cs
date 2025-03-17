using FinanzautoShool.Aplication.Services.TeacherService.Interface;
using FinanzautoShool.Domain.Dto;
using FinanzautoShool.Domain.Entity;
using FinanzautoShool.Infrastructure.Repositories.TeacherRepositories.Interface;

namespace FinanzautoShool.Aplication.Services.TeacherService.Service
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _repository;

        public TeacherService(ITeacherRepository repository)
        {
            _repository = repository;
        }

        public async Task<TeacherDto> CreateTeacher(CreateTeacherDto createCategoryDto)
        {
            var teacher = new Teacher
            {
                FirstName = createCategoryDto.FirstName,
                LastName = createCategoryDto.LastName,
                Email = createCategoryDto.Email,
                Specialty = createCategoryDto.Specialty,
                CreatedAt = DateTime.Now,
            };

            var createdTeacher = await _repository.CreateTeacher(teacher);

            return new TeacherDto
            {
                Id = createdTeacher.Id,
                FirstName = createdTeacher.FirstName,
                LastName = createdTeacher.LastName,
                Email = createdTeacher.Email,
                Specialty = createdTeacher.Specialty,
            };
        }

        public async Task<DeleteResult> DeleteTeacher(int id)
        {
            var teacher = await _repository.GetTeacherById(id);
            if (teacher == null)
            {
                return new DeleteResult
                {
                    Message = "No se encontró un profesor con el ID proporcionado.",
                    IsSuccess = false
                };
            }

            var isDeleted = await _repository.DeleteTeacher(id);
            if (!isDeleted)
            {
                return new DeleteResult
                {
                    Message = "No se pudo eliminar el profesor.",
                    IsSuccess = false
                };
            }

            return new DeleteResult
            {
                Message = "El profesor fue eliminado correctamente.",
                IsSuccess = true
            };
        }

        public async Task<IEnumerable<TeacherDto>> GetAllTeacher()
        {
            var teacher = await _repository.GetAllTeacher();
            return teacher.Select(t => new TeacherDto
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                Specialty = t.Specialty,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            });
        }

        public async Task<TeacherDto> GetTeacherById(int id)
        {
            var teacher = await _repository.GetTeacherById(id);
            if (teacher == null) return null;

            return new TeacherDto
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Specialty = teacher.Specialty,
                CreatedAt = teacher.CreatedAt,
                UpdatedAt = teacher.UpdatedAt
            };
        }

        public async Task<int?> UpdateTeacher(int id, UpdateTeacherDto updateTeacherDto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del profesor es obligatorio y debe ser un número válido.");

            var existingTeacher = await _repository.GetTeacherById(id);
            if (existingTeacher == null)
                return null;

            if (updateTeacherDto.FirstName != null)
                existingTeacher.FirstName = updateTeacherDto.FirstName;

            if (updateTeacherDto.LastName != null)
                existingTeacher.LastName = updateTeacherDto.LastName;

            if (updateTeacherDto.Email != null)
                existingTeacher.Email = updateTeacherDto.Email;

            if (updateTeacherDto.Specialty != null)
                existingTeacher.Specialty = updateTeacherDto.Specialty;

            existingTeacher.UpdatedAt = DateTime.Now;

            var isUpdated = await _repository.UpdateTeacher(existingTeacher);
            if (!isUpdated)
                return null;

            return existingTeacher.Id;
        }
    }
}
