using System;
using System.Collections.Generic;

namespace ResumeManagement_API.Models;

public partial class UserMaster
{
    public Guid UserMasterId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public sbyte IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
