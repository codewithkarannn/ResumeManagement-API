using System;
using System.Collections.Generic;

namespace ResumeManagement_API.Models;

public partial class CountryMaster
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public virtual ICollection<CityMaster> CityMasters { get; set; } = new List<CityMaster>();
}
