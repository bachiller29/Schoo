using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanzautoShool.Aplication.Services.CourseService.Interface;
using FinanzautoShool.Aplication.Services.QualificationService.Interface;
using FinanzautoShool.Aplication.Services.StudentService.Interface;
using FinanzautoShool.Domain.Dto;
using FinanzautoShool.Domain.Entity;
using FinanzautoShool.Infrastructure.Repositories.QualificationRepositories.Interface;

namespace FinanzautoShool.Aplication.Services.QualificationService.Service
{
    public class QualificationService : IQualificationService
    {
        private readonly IQualificationRepository _repository;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public QualificationService(IQualificationRepository repository, IStudentService student, ICourseService course)
        {
            _repository = repository;
            _courseService = course;
            _studentService = student;
        }

        public async Task<QualificationDto> CreateQualification(CreateQualificationDto qualificationDto)
        {
            var studentExists = await _studentService.GetStudentById(qualificationDto.StudentId);

            if (studentExists == null)
            {
                throw new ArgumentException($"El estudiante con ID {qualificationDto.StudentId} no existe.");
            }

            var courseExists = await _courseService.GetCourseById(qualificationDto.CourseId);

            if (courseExists == null)
            {
                throw new ArgumentException($"El profesor con ID {qualificationDto.CourseId} no existe.");
            }

            var qualification = new Qualification
            {
                CourseId = qualificationDto.CourseId,
                StudentId = qualificationDto.StudentId,
                Score = qualificationDto.Score,
                QualificationDate = DateTime.Now,
                CreatedAt = DateTime.Now,
            };

            var createdQualification = await _repository.CreateQualification(qualification);

            return new QualificationDto
            {
                CourseId = createdQualification.CourseId,
                StudentId = createdQualification.StudentId,
                Score = createdQualification.Score,
                CreatedAt = createdQualification.CreatedAt,
                QualificationDate = createdQualification.CreatedAt,
            };
        }

        public async Task<DeleteResult> DeleteQualification(int id)
        {
            var qualification = await _repository.GetQualificationById(id);
            if (qualification == null)
            {
                return new DeleteResult
                {
                    Message = "No se encontró una nota con el ID proporcionado.",
                    IsSuccess = false
                };
            }

            var isDeleted = await _repository.DeleteQualification(id);
            if (!isDeleted)
            {
                return new DeleteResult
                {
                    Message = "No se pudo eliminar la nota.",
                    IsSuccess = false
                };
            }

            return new DeleteResult
            {
                Message = "La nota fue eliminado correctamente.",
                IsSuccess = true
            };
        }

        public async Task<IEnumerable<QualificationDto>> GetAllQualification()
        {
            var qualifications = await _repository.GetAllQualification();
            return qualifications.Select(q => new QualificationDto
            {
                Id = q.Id,
                CourseId = q.CourseId,
                StudentId = q.StudentId,
                Score = q.Score,
                QualificationDate = q.QualificationDate,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            });
        }

        public async Task<QualificationDto> GetQualificationById(int id)
        {
            var qualification = await _repository.GetQualificationById(id);
            if (qualification == null) return null;

            return new QualificationDto
            {
                Id = qualification.Id,
                CourseId = qualification.CourseId,
                StudentId = qualification.StudentId,
                Score = qualification.Score,
                QualificationDate = qualification.QualificationDate,
                CreatedAt = qualification.CreatedAt,
                UpdatedAt = qualification.UpdatedAt
            };
        }

        public async Task<int?> UpdateQualification(int id, UpdateQualificationDto updateQualificationDto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la nota es obligatorio y debe ser un número válido.");

            var existingQualification = await _repository.GetQualificationById(id);
            if (existingQualification == null)
                return null;

            // Validar si el StudentId existe antes de actualizar
            if (updateQualificationDto.StudentId.HasValue)
            {
                var studentExists = await _studentService.GetStudentById(updateQualificationDto.StudentId.Value);
                if (studentExists == null)
                    throw new ArgumentException($"El estudiante con ID {updateQualificationDto.StudentId} no existe.");

                existingQualification.StudentId = updateQualificationDto.StudentId.Value;
            }

            // Validar si el CourseId existe antes de actualizar
            if (updateQualificationDto.CourseId.HasValue)
            {
                var courseExists = await _courseService.GetCourseById(updateQualificationDto.CourseId.Value);
                if (courseExists == null)
                    throw new ArgumentException($"El curso con ID {updateQualificationDto.CourseId.Value} no existe.");

                existingQualification.CourseId = updateQualificationDto.CourseId.Value;
            }

            if (updateQualificationDto.StudentId.HasValue)
            {
                existingQualification.StudentId = updateQualificationDto.StudentId.Value;
            }

            if (updateQualificationDto.CourseId.HasValue)
            {
                existingQualification.CourseId = updateQualificationDto.CourseId.Value;
            }

            if (updateQualificationDto.Score.HasValue)
                existingQualification.Score = updateQualificationDto.Score.Value;

            if (updateQualificationDto.QualificationDate.HasValue)
            {
                existingQualification.QualificationDate = updateQualificationDto.QualificationDate.Value;
            }

            existingQualification.UpdatedAt = DateTime.Now;

            var isUpdated = await _repository.UpdateQualification(existingQualification);
            if (!isUpdated)
                return null;

            return existingQualification.Id;
        }
    }
}
