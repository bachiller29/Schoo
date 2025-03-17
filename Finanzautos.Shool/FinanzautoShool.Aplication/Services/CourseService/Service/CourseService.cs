using FinanzautoShool.Aplication.Services.CourseService.Interface;
using FinanzautoShool.Domain.Dto;
using FinanzautoShool.Domain.Entity;
using FinanzautoShool.Infrastructure.Repositories.CourseRepositories.Interface;
using FinanzautoShool.Infrastructure.Repositories.TeacherRepositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FinanzautoShool.Aplication.Services.CourseService.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;
        private readonly ITeacherRepository _teacherRepository;

        public CourseService(ICourseRepository repository, ITeacherRepository teacherRepository)
        {
            _repository = repository;
            _teacherRepository = teacherRepository;
        }
        public async Task<CourseDto> CreateCourse(CreateCourseDto courseDto)
        {
            var teacherExists = await _teacherRepository.GetTeacherById(courseDto.TeacherId);

            if (teacherExists == null)
            {
                throw new ArgumentException($"El profesor con ID {courseDto.TeacherId} no existe.");
            }

            var course = new Course
            {
                CourseName = courseDto.CourseName,
                Description = courseDto.Description,
                TeacherId = courseDto.TeacherId,
                CreatedAt = DateTime.Now,
            };

            var createdCourse = await _repository.CreateCourse(course);

            return new CourseDto
            {
                CourseName = course.CourseName,
                Description = course.Description,
                TeacherId = course.TeacherId,
                CreatedAt = course.CreatedAt,
            };
        }

        public async Task<DeleteResult> DeleteCourse(int id)
        {
            var course = await _repository.GetCourseById(id);
            if (course == null)
            {
                return new DeleteResult
                {
                    Message = "No se encontró un curso con el ID proporcionado.",
                    IsSuccess = false
                };
            }

            try
            {
                var isDeleted = await _repository.DeleteCourse(id);
                if (!isDeleted)
                {
                    return new DeleteResult
                    {
                        Message = "No se pudo eliminar el curso.",
                        IsSuccess = false
                    };
                }

                return new DeleteResult
                {
                    Message = "El curso fue eliminado correctamente.",
                    IsSuccess = true
                };
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                return new DeleteResult
                {
                    Message = "No se puede eliminar el curso porque tiene calificaciones asociadas. Elimine primero las calificaciones.",
                    IsSuccess = false
                };
            }
        }

        public async Task<IEnumerable<CourseDto>> GetAllCourse()
        {
            var course = await _repository.GetAllCourse();
            return course.Select(c => new CourseDto
            {
                Id = c.Id,
                CourseName = c.CourseName,
                Description = c.Description,
                TeacherId = c.TeacherId,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
            });
        }

        public async Task<CourseDto> GetCourseById(int id)
        {
            var course = await _repository.GetCourseById(id);
            if (course == null) return null;

            return new CourseDto
            {
                Id = course.Id,
                CourseName = course.CourseName,
                Description = course.Description,
                TeacherId = course.TeacherId,
                CreatedAt = course.CreatedAt,
                UpdatedAt = course.UpdatedAt,
            };
        }

        public async Task<int?> UpdateCourse(int id, UpdateCourseDto updateCourseDto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del curso es obligatorio y debe ser un número válido.");

            var existingCourse = await _repository.GetCourseById(id);
            if (existingCourse == null)
                return null;

            // Validar si el CourseId existe antes de actualizar
            if (updateCourseDto.TeacherId.HasValue)
            {
                var courseExists = await _teacherRepository.GetTeacherById(updateCourseDto.TeacherId.Value);
                if (courseExists == null)
                    throw new ArgumentException($"El curso con ID {updateCourseDto.TeacherId.Value} no existe.");

                existingCourse.TeacherId = updateCourseDto.TeacherId.Value;
            }

            if (updateCourseDto.CourseName != null)
                existingCourse.CourseName = updateCourseDto.CourseName;

            if (updateCourseDto.Description != null)
                existingCourse.Description = updateCourseDto.Description;

            if (updateCourseDto.TeacherId.HasValue)
            {
                existingCourse.TeacherId = updateCourseDto.TeacherId.Value;
            }

            existingCourse.UpdatedAt = DateTime.Now;

            var isUpdated = await _repository.UpdateCourse(existingCourse);
            if (!isUpdated)
                return null;

            return existingCourse.Id;
        }
    }
}
