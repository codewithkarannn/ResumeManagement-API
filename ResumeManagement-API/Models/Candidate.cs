using System;
using System.Collections.Generic;

namespace ResumeManagement_API.Models;

public partial class Candidate
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

    public int? CityId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public sbyte? IsActive { get; set; }

    public virtual ICollection<CandidateCvfile> CandidateCvfiles { get; set; } = new List<CandidateCvfile>();

    public virtual CityMaster? City { get; set; }

    public virtual StatusMaster? Status { get; set; }
}
