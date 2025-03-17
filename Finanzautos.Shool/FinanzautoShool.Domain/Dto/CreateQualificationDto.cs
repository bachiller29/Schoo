using System.ComponentModel.DataAnnotations;

namespace FinanzautoShool.Domain.Dto
{
    public class CreateQualificationDto
    {
        [Required(ErrorMessage = "El ID del estudiante es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del estudiante debe ser un número positivo.")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "El ID del curso es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del curso debe ser un número positivo.")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "La nota es obligatorio.")]
        [Range(0, 5, ErrorMessage = "La nota debe estar entre 0 y 100.")]
        public decimal Score { get; set; }
    }
}
