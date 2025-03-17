using System.ComponentModel.DataAnnotations;

namespace FinanzautoShool.Domain.Dto
{
    public class CreateTeacherDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La especialidad es obligatoria.")]
        [MaxLength(100, ErrorMessage = "La especialidad no puede tener más de 100 caracteres.")]
        public string Specialty { get; set; }
    }
}
