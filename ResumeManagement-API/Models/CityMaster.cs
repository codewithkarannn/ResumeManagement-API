using System;
using System.Collections.Generic;

namespace ResumeManagement_API.Models;

public partial class CityMaster
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public int? CountryId { get; set; }

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

    public virtual CountryMaster? Country { get; set; }
}
