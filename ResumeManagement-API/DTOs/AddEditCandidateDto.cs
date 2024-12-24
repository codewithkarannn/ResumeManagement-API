using System.ComponentModel.DataAnnotations;

namespace ResumeManagement_API.DTOs
{
    public class AddEditCandidateDto
    {
        public Guid? CandidateId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(250, ErrorMessage = "Name must be less than 250 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;

        [Phone(ErrorMessage = "Invalid mobile number.")]
        public string? Mobile { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "CTC must be a positive value.")]
        public decimal? Ctc { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Expected CTC must be a positive value.")]
        public decimal? ExpectedCtc { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid interview date.")]
        public DateTime? InterviewDate { get; set; }

        [StringLength(1000, ErrorMessage = "Feedback must be less than 1000 characters.")]
        public string? InterviewFeedback { get; set; }

        [Required(ErrorMessage = "Status ID is required.")]
        public int? StatusId { get; set; }

        [Required(ErrorMessage = "City ID is required.")]
        public int? CityId { get; set; }


        public IFormFile? CvFile { get; set; } // New property to accept file
        public DateTime? CreatedAt { get; set; }
    }
}
