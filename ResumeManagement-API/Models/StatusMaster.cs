using System;
using System.Collections.Generic;

namespace ResumeManagement_API.Models;

public partial class StatusMaster
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
}
