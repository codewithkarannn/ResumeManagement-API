using System;
using System.Collections.Generic;

namespace ResumeManagement_API.Models;

public partial class CandidateCvfile
{
    public Guid FileId { get; set; }

    public Guid? CandidateId { get; set; }

    public string? FileName { get; set; }

    public string? FileType { get; set; }

    public long? FileSize { get; set; }

    public byte[]? FileData { get; set; }

    public DateTime? CreatedAt { get; set; }

    public sbyte IsActive { get; set; }

    public virtual Candidate? Candidate { get; set; }
}
