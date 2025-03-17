namespace FinanzautoShool.Domain.Dto
{
    public class UpdateQualificationDto
    {
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }
        public decimal? Score { get; set; }
        public DateTime? QualificationDate { get; set; }
    }
}
