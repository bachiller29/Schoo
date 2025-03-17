using System.ComponentModel.DataAnnotations;

namespace FinanzautoShool.Domain.Dto
{
    public class CreateCourseDto
    {
        [Required(ErrorMessage = "El nombre del curso es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre del curso no puede tener más de 100 caracteres.")]
        public string CourseName { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "El ID del profesor es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del profesor debe ser un número positivo.")]
        public int TeacherId { get; set; }
    }
}
