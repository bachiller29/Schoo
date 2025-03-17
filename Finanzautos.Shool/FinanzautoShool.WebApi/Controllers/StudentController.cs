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

        /// <summary>
        /// Crea un nuevo estudiante en el sistema.
        /// </summary>
        /// <param name="createStudent">Objeto que contiene la información del estudiante a crear.</param>
        /// <returns>Retorna el estudiante creado con su ID.</returns>
        /// <response code="201">Estudiante creado exitosamente.</response>
        /// <response code="400">Datos de entrada inválidos.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <example>
        /// Ejemplo de petición JSON:
        /// {
        ///     "firstName": "Juan",
        ///     "lastName": "Pérez",
        ///     "birthDate": "2000-05-15",
        ///     "email": "juan.perez@example.com"
        /// }
        /// </example>
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

        /// <summary>
        /// Obtiene la lista de todos los estudiantes registrados.
        /// </summary>
        /// <returns>Lista de estudiantes en el sistema.</returns>
        /// <response code="200">Lista obtenida correctamente.</response>
        /// <example>
        /// Ejemplo de respuesta JSON:
        /// [
        ///     {
        ///         "id": 1,
        ///         "firstName": "Juan",
        ///         "lastName": "Pérez",
        ///         "birthDate": "2000-05-15",
        ///         "email": "juan.perez@example.com"
        ///     }
        /// ]
        /// </example>
        [HttpGet("GetAllStudent")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudent()
        {
            var student = await _studentService.GetAllStudent();
            return Ok(student);
        }

        /// <summary>
        /// Obtiene la información de un estudiante específico por su ID.
        /// </summary>
        /// <param name="id">ID del estudiante a consultar.</param>
        /// <returns>Información del estudiante solicitado.</returns>
        /// <response code="200">Estudiante encontrado.</response>
        /// <response code="404">Estudiante no encontrado.</response>
        /// <example>
        /// Ejemplo de respuesta JSON:
        /// {
        ///     "id": 1,
        ///     "firstName": "Juan",
        ///     "lastName": "Pérez",
        ///     "birthDate": "2000-05-15",
        ///     "email": "juan.perez@example.com"
        /// }
        /// </example>
        [HttpGet("GetStudentById/{id}")]
        public async Task<ActionResult<TeacherDto>> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentById(id);
            if (student == null) return NotFound("Estudiante no encontrado.");
            return Ok(student);
        }

        /// <summary>
        /// Elimina un estudiante por su ID.
        /// </summary>
        /// <param name="id">ID del estudiante a eliminar.</param>
        /// <returns>Mensaje de confirmación o error.</returns>
        /// <response code="200">Estudiante eliminado exitosamente.</response>
        /// <response code="404">Estudiante no encontrado.</response>
        /// <response code="500">Error interno al intentar eliminar el estudiante.</response>
        /// <example>
        /// Ejemplo de respuesta JSON:
        /// {
        ///     "message": "El estudiante fue eliminado correctamente."
        /// }
        /// </example>
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

        /// <summary>
        /// Actualiza la información de un estudiante existente.
        /// </summary>
        /// <param name="id">ID del estudiante a actualizar.</param>
        /// <param name="updateStudentDto">Objeto con los nuevos datos del estudiante.</param>
        /// <returns>Mensaje de confirmación o error.</returns>
        /// <response code="200">Estudiante actualizado correctamente.</response>
        /// <response code="400">Datos de entrada inválidos.</response>
        /// <response code="404">Estudiante no encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <example>
        /// Ejemplo de petición JSON:
        /// {
        ///     "firstName": "Carlos",
        ///     "lastName": "Gómez",
        ///     "email": "carlos.gomez@example.com"
        /// }
        /// </example>
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
