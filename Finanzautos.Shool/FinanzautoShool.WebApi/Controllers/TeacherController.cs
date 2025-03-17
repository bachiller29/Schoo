using FinanzautoShool.Aplication.Services.TeacherService.Interface;
using FinanzautoShool.Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanzautoShool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost("CreateTeacher")]
        public async Task<ActionResult<TeacherDto>> CreateTeacher([FromBody] CreateTeacherDto createTeacher)
        {
            try
            {
                var teacher = await _teacherService.CreateTeacher(createTeacher);
                return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.Id }, teacher);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocurrió un error interno al crear el profesor." });
            }
        }

        [HttpGet("GetAllTeacher")]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAllTeacher()
        {
            var categories = await _teacherService.GetAllTeacher();
            return Ok(categories);
        }

        [HttpGet("GetTeacherById/{id}")]
        public async Task<ActionResult<TeacherDto>> GetTeacherById(int id)
        {
            var teacher = await _teacherService.GetTeacherById(id);
            if (teacher == null) return NotFound("Profesor no encontrado.");
            return Ok(teacher);
        }

        [HttpDelete("DeleteTeacher/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var result = await _teacherService.DeleteTeacher(id);

            if (result.Message == "Profesor no encontrado.")
                return NotFound(result);

            if (result.Message == "No se pudo eliminar el profesor.")
                return StatusCode(500, result);

            return Ok(result);
        }

        [HttpPut("UpdateTeacher/{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] UpdateTeacherDto updateTeacherDto)
        {
            try
            {
                var updatedTeacherId = await _teacherService.UpdateTeacher(id, updateTeacherDto);

                if (updatedTeacherId == null)
                    return NotFound("Profesor no encontrado.");

                return Ok(new { Id = updatedTeacherId, Message = "Profesor actualizado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocurrió un error interno al actualizar el profesor." });
            }
        }
    }
}
