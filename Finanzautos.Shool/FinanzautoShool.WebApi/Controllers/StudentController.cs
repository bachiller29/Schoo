using FinanzautoShool.Aplication.Services.StudentService.Interface;
using FinanzautoShool.Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanzautoShool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("CreateStudent")]
        public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] CreateStudentDto createStudent)
        {
            try
            {
                var student = await _studentService.CreateStudent(createStudent);
                return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocurrió un error interno al crear el estudiante." });
            }
        }

        [HttpGet("GetAllStudent")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudent()
        {
            var student = await _studentService.GetAllStudent();
            return Ok(student);
        }

        [HttpGet("GetStudentById/{id}")]
        public async Task<ActionResult<TeacherDto>> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentById(id);
            if (student == null) return NotFound("Estudiante no encontrado.");
            return Ok(student);
        }

        [HttpDelete("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _studentService.DeleteStudent(id);

            if (result.Message == "Estudiante no encontrado.")
                return NotFound(result);

            if (result.Message == "No se pudo eliminar el estudiante.")
                return StatusCode(500, result);

            return Ok(result);
        }

        [HttpPut("UpdateStudent/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            try
            {
                var updatedStudent = await _studentService.UpdateStudent(id, updateStudentDto);

                if (updatedStudent == null)
                    return NotFound("Estudiante no encontrado.");

                return Ok(new { Id = updatedStudent, Message = "Estudiante actualizado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocurrió un error interno al actualizar el estudainte." });
            }
        }
    }
}
