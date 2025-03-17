using FinanzautoShool.Aplication.Services.CourseService.Interface;
using FinanzautoShool.Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanzautoShool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("CreateCourse")]
        public async Task<ActionResult<CourseDto>> CreateCourse([FromBody] CreateCourseDto createCourse)
        {
            try
            {
                var course = await _courseService.CreateCourse(createCourse);
                return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocurrió un error interno al crear el curso." });
            }
        }

        [HttpGet("GetAllCourse")]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAllCourse()
        {
            var courses = await _courseService.GetAllCourse();
            return Ok(courses);
        }

        [HttpGet("GetCourseById/{id}")]
        public async Task<ActionResult<TeacherDto>> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseById(id);
            if (course == null) return NotFound("Curso no encontrado.");
            return Ok(course);
        }

        [HttpDelete("DeleteCourse/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseService.DeleteCourse(id);

            if (result.Message == "Curso no encontrado.")
                return NotFound(result);

            if (result.Message == "No se pudo eliminar el curso.")
                return StatusCode(500, result);

            return Ok(result);
        }

        [HttpPut("UpdateCourse/{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto updateCourseDto)
        {
            try
            {
                var updatedCourseId = await _courseService.UpdateCourse(id, updateCourseDto);

                if (updatedCourseId == null)
                    return NotFound("Curso no encontrado.");

                return Ok(new { Id = updatedCourseId, Message = "Curso actualizado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocurrió un error interno al actualizar el curso." });
            }
        }
    }
}
