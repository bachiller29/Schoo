using FinanzautoShool.Aplication.Services.CourseService.Interface;
using FinanzautoShool.Domain.Dto;
using FinanzautoShool.Infrastructure.Repositories.CourseRepositories.Interface;
using FinanzautoShool.Infrastructure.Repositories.TeacherRepositories.Interface;

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
        public async Task<CourseDto> CreateCourse(CreateCourseDto createCourseDto)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteResult> DeleteCourse(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CourseDto>> GetAllCourse()
        {
            throw new NotImplementedException();
        }

        public async Task<CourseDto> GetCourseById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCourse(int id, UpdateCourseDto updateCourseDto)
        {
            throw new NotImplementedException();
        }
    }
}
