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

        /// <summary>
        /// Crea un nuevo curso en el sistema.
        /// </summary>
        /// <param name="createCourse">Objeto que contiene la información del curso a crear.</param>
        /// <returns>
        /// Retorna el curso creado con su ID.
        /// </returns>
        /// <response code="201">Curso creado exitosamente.</response>
        /// <response code="400">Datos de entrada inválidos.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <example>
        /// Ejemplo de petición JSON:
        /// {
        ///     "courseName": "Matemáticas",
        ///     "description": "Curso de matemáticas avanzadas",
        ///     "teacherId": 1
        /// }
        /// </example>
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

        /// <summary>
        /// Obtiene la lista de todos los cursos disponibles.
        /// </summary>
        /// <returns>Lista de cursos registrados en el sistema.</returns>
        /// <response code="200">Lista de cursos obtenida correctamente.</response>
        /// <example>
        /// Ejemplo de respuesta JSON:
        /// [
        ///     {
        ///         "id": 1,
        ///         "courseName": "Matemáticas",
        ///         "description": "Curso de matemáticas avanzadas",
        ///         "teacherId": 1
        ///     }
        /// ]
        /// </example>
        [HttpGet("GetAllCourse")]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAllCourse()
        {
            var courses = await _courseService.GetAllCourse();
            return Ok(courses);
        }

        /// <summary>
        /// Obtiene la información de un curso específico por su ID.
        /// </summary>
        /// <param name="id">ID del curso a consultar.</param>
        /// <returns>Información del curso solicitado.</returns>
        /// <response code="200">Curso encontrado.</response>
        /// <response code="404">Curso no encontrado.</response>
        /// <example>
        /// Ejemplo de respuesta JSON:
        /// {
        ///     "id": 1,
        ///     "courseName": "Matemáticas",
        ///     "description": "Curso de matemáticas avanzadas",
        ///     "teacherId": 1
        /// }
        /// </example>
        [HttpGet("GetCourseById/{id}")]
        public async Task<ActionResult<TeacherDto>> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseById(id);
            if (course == null) return NotFound("Curso no encontrado.");
            return Ok(course);
        }

        /// <summary>
        /// Elimina un curso por su ID.
        /// </summary>
        /// <param name="id">ID del curso a eliminar.</param>
        /// <returns>Mensaje de confirmación o error.</returns>
        /// <response code="200">Curso eliminado exitosamente.</response>
        /// <response code="404">Curso no encontrado.</response>
        /// <response code="500">Error interno al intentar eliminar el curso.</response>
        /// <example>
        /// Ejemplo de respuesta JSON:
        /// {
        ///     "message": "El curso fue eliminado correctamente."
        /// }
        /// </example>
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

        /// <summary>
        /// Actualiza la información de un curso existente.
        /// </summary>
        /// <param name="id">ID del curso a actualizar.</param>
        /// <param name="updateCourseDto">Objeto con los nuevos datos del curso.</param>
        /// <returns>Mensaje de confirmación o error.</returns>
        /// <response code="200">Curso actualizado correctamente.</response>
        /// <response code="400">Datos de entrada inválidos.</response>
        /// <response code="404">Curso no encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <example>
        /// Ejemplo de petición JSON:
        /// {
        ///     "courseName": "Física",
        ///     "description": "Curso de física cuántica",
        ///     "teacherId": 2
        /// }
        /// </example>
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
