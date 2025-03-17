using System.Security.AccessControl;

namespace FinanzautoShool.Domain.Entity
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int TeacherId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
