using FinanzautoShool.Aplication.Services.QualificationService.Interface;
using FinanzautoShool.Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanzautoShool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualificationController : ControllerBase
    {
        private readonly IQualificationService _qualificationService;

        public QualificationController(IQualificationService studentService)
        {
            _qualificationService = studentService;
        }

        /// <summary>
        /// Crea una nueva calificación en el sistema.
        /// </summary>
        /// <param name="createQualification">Objeto que contiene la información de la calificación a crear.</param>
        /// <returns>Retorna la calificación creada con su ID.</returns>
        /// <response code="201">Calificación creada exitosamente.</response>
        /// <response code="400">Datos de entrada inválidos.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <example>
        /// Ejemplo de petición JSON:
        /// {
        ///     "studentId": 1,
        ///     "courseId": 2,
        ///     "score": 85
        /// }
        /// </example>
        [HttpPost("CreateQualification")]
        public async Task<ActionResult<QualificationDto>> CreateQualification([FromBody] CreateQualificationDto createQualification)
        {
            try
            {
                var qualification = await _qualificationService.CreateQualification(createQualification);
                return CreatedAtAction(nameof(GetQualificationById), new { id = qualification.Id }, qualification);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocurrió un error interno al crear la calificacion." });
            }
        }

        /// <summary>
        /// Obtiene la lista de todas las calificaciones registradas.
        /// </summary>
        /// <returns>Lista de calificaciones en el sistema.</returns>
        /// <response code="200">Lista obtenida correctamente.</response>
        /// <example>
        /// Ejemplo de respuesta JSON:
        /// [
        ///     {
        ///         "id": 1,
        ///         "studentId": 1,
        ///         "courseId": 2,
        ///         "score": 85,
        ///         "qualificationDate": "2025-03-01T10:00:00Z"
        ///     }
        /// ]
        /// </example>
        [HttpGet("GetAllQualification")]
        public async Task<ActionResult<IEnumerable<QualificationDto>>> GetAllQualification()
        {
            var qualification = await _qualificationService.GetAllQualification();
            return Ok(qualification);
        }

        /// <summary>
        /// Obtiene la información de una calificación específica por su ID.
        /// </summary>
        /// <param name="id">ID de la calificación a consultar.</param>
        /// <returns>Información de la calificación solicitada.</returns>
        /// <response code="200">Calificación encontrada.</response>
        /// <response code="404">Calificación no encontrada.</response>
        /// <example>
        /// Ejemplo de respuesta JSON:
        /// {
        ///     "id": 1,
        ///     "studentId": 1,
        ///     "courseId": 2,
        ///     "score": 85,
        ///     "qualificationDate": "2025-03-01T10:00:00Z"
        /// }
        /// </example>
        [HttpGet("GetQualificationById/{id}")]
        public async Task<ActionResult<TeacherDto>> GetQualificationById(int id)
        {
            var qualification = await _qualificationService.GetQualificationById(id);
            if (qualification == null) return NotFound("calificacion no encontrado.");
            return Ok(qualification);
        }

        /// <summary>
        /// Elimina una calificación por su ID.
        /// </summary>
        /// <param name="id">ID de la calificación a eliminar.</param>
        /// <returns>Mensaje de confirmación o error.</returns>
        /// <response code="200">Calificación eliminada exitosamente.</response>
        /// <response code="404">Calificación no encontrada.</response>
        /// <response code="500">Error interno al intentar eliminar la calificación.</response>
        /// <example>
        /// Ejemplo de respuesta JSON:
        /// {
        ///     "message": "La calificación fue eliminada correctamente."
        /// }
        /// </example>
        [HttpDelete("DeleteQualification/{id}")]
        public async Task<IActionResult> DeleteQualification(int id)
        {
            var result = await _qualificationService.DeleteQualification(id);

            if (result.Message == "Calificacion no encontrado.")
                return NotFound(result);

            if (result.Message == "No se pudo eliminar la calificacion.")
                return StatusCode(500, result);

            return Ok(result);
        }

        /// <summary>
        /// Actualiza la información de una calificación existente.
        /// </summary>
        /// <param name="id">ID de la calificación a actualizar.</param>
        /// <param name="updateQualificationDto">Objeto con los nuevos datos de la calificación.</param>
        /// <returns>Mensaje de confirmación o error.</returns>
        /// <response code="200">Calificación actualizada correctamente.</response>
        /// <response code="400">Datos de entrada inválidos.</response>
        /// <response code="404">Calificación no encontrada.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <example>
        /// Ejemplo de petición JSON:
        /// {
        ///     "score": 90,
        ///     "qualificationDate": "2025-03-02T12:00:00Z"
        /// }
        /// </example>
        [HttpPut("UpdateQualification/{id}")]
        public async Task<IActionResult> UpdateQualification(int id, [FromBody] UpdateQualificationDto updateQualificationDto)
        {
            try
            {
                var updateQualification = await _qualificationService.UpdateQualification(id, updateQualificationDto);

                if (updateQualification == null)
                    return NotFound("Calificacion no encontrado.");

                return Ok(new { Id = updateQualification, Message = "Calificacion actualizado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocurrió un error interno al actualizar la nota." });
            }
        }
    }
}
