namespace ResumeManagement_API.DTOs
{
    public class CandidateDto
    {
        public Guid CandidateId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Mobile { get; set; }

        public decimal? Ctc { get; set; }

        public decimal? ExpectedCtc { get; set; }

        public DateTime? InterviewDate { get; set; }

        public string? InterviewFeedback { get; set; }

        public int? StatusId { get; set; }
        public string? StatusName { get; set; }

        public int? CityId { get; set; }
        public string? CityName { get; set; }

        public DateTime? CreatedAt { get; set; }

        public sbyte? IsActive { get; set; }

        public string? CvFileName { get; set; }

        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
    }

}
