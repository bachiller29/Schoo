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

        [HttpGet("GetAllQualification")]
        public async Task<ActionResult<IEnumerable<QualificationDto>>> GetAllQualification()
        {
            var qualification = await _qualificationService.GetAllQualification();
            return Ok(qualification);
        }

        [HttpGet("GetQualificationById/{id}")]
        public async Task<ActionResult<TeacherDto>> GetQualificationById(int id)
        {
            var qualification = await _qualificationService.GetQualificationById(id);
            if (qualification == null) return NotFound("calificacion no encontrado.");
            return Ok(qualification);
        }

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
