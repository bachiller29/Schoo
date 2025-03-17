namespace FinanzautoShool.Domain.Entity
{
    public class Qualification
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal Score { get; set; }
        public DateTime QualificationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
