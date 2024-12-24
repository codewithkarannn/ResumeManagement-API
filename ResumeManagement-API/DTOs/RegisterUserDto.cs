﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ResumeManagement_API.DTOs
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?\""':;{}|<>])[A-Za-z\d!@#$%^&*(),.?\""':;{}|<>]{6,}$", ErrorMessage = "Password must contain at least one uppercase letter, one number, and one special character.")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
