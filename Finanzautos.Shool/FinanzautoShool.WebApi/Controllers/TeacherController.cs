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

        /// <summary>
        /// Crea un nuevo profesor en el sistema.
        /// </summary>
        /// <param name="createTeacher">Objeto que contiene la información del profesor a crear.</param>
        /// <returns>Retorna el profesor creado con su ID.</returns>
        /// <response code="201">Profesor creado exitosamente.</response>
        /// <response code="400">Datos de entrada inválidos.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <example>
        /// POST /CreateTeacher
        /// {
        ///   "firstName": "Juan",
        ///   "lastName": "Perez",
        ///   "email": "juan.perez@example.com",
        ///   "specialty": "Matemáticas"
        /// }
        /// </example>
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

        /// <summary>
        /// Obtiene la lista de todos los profesores registrados.
        /// </summary>
        /// <returns>Lista de profesores en el sistema.</returns>
        /// <response code="200">Lista obtenida correctamente.</response>
        /// <example>
        /// GET /GetAllTeacher
        /// </example>
        [HttpGet("GetAllTeacher")]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAllTeacher()
        {
            var categories = await _teacherService.GetAllTeacher();
            return Ok(categories);
        }

        /// <summary>
        /// Obtiene la información de un profesor específico por su ID.
        /// </summary>
        /// <param name="id">ID del profesor a consultar.</param>
        /// <returns>Información del profesor solicitado.</returns>
        /// <response code="200">Profesor encontrado.</response>
        /// <response code="404">Profesor no encontrado.</response>
        /// <example>
        /// GET /GetTeacherById/1
        /// </example>
        [HttpGet("GetTeacherById/{id}")]
        public async Task<ActionResult<TeacherDto>> GetTeacherById(int id)
        {
            var teacher = await _teacherService.GetTeacherById(id);
            if (teacher == null) return NotFound("Profesor no encontrado.");
            return Ok(teacher);
        }

        /// <summary>
        /// Elimina un profesor por su ID.
        /// </summary>
        /// <param name="id">ID del profesor a eliminar.</param>
        /// <returns>Mensaje de confirmación o error.</returns>
        /// <response code="200">Profesor eliminado exitosamente.</response>
        /// <response code="404">Profesor no encontrado.</response>
        /// <response code="500">Error interno al intentar eliminar el profesor.</response>
        /// <example>
        /// DELETE /DeleteTeacher/1
        /// </example>
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

        /// <summary>
        /// Actualiza la información de un profesor existente.
        /// </summary>
        /// <param name="id">ID del profesor a actualizar.</param>
        /// <param name="updateTeacherDto">Objeto con los nuevos datos del profesor.</param>
        /// <returns>Mensaje de confirmación o error.</returns>
        /// <response code="200">Profesor actualizado correctamente.</response>
        /// <response code="400">Datos de entrada inválidos.</response>
        /// <response code="404">Profesor no encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <example>
        /// PUT /UpdateTeacher/1
        /// {
        ///   "firstName": "Carlos",
        ///   "lastName": "Gomez",
        ///   "email": "carlos.gomez@example.com",
        ///   "specialty": "Física"
        /// }
        /// </example>
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
