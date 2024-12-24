using Microsoft.IdentityModel.Tokens;
using ResumeManagement_API.DTOs;
using ResumeManagement_API.Models;
using ResumeManagement_API.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ResumeManagement_API.Services
{
    public class AuthService : IAuthServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;


        public AuthService(IUserRepository userRepository ,IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task ResgisterUserAsync(RegisterUserDto registerUserDto)
        {
            try
            {
                if(await _userRepository.IsEmailExistsAsync(registerUserDto.Email)) {

                    throw new ArgumentException("Email already exists. Please use a different email.");

                }
                
                var passwordHash = HashPassword(registerUserDto.Password);

                var user = new UserMaster
                {   
                    
                    PasswordHash = passwordHash,
                    Email = registerUserDto.Email,
                    Role = registerUserDto.Role ?? "User"
                };

                await _userRepository.AddUserAsync(user);
            }
            catch(ArgumentException ex)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new Exception("There is the problem in signing up. Please try agian later", ex);
            }
        }

        public async Task<string> LoginUserAsync(LoginUserDto loginUserDto)
        {
            // Validate user credentials
            var user = await _userRepository.GetUserByEmailAsync(loginUserDto.Email);

            if (user == null || !VerifyPassword(loginUserDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            // Generate JWT Token
            var token = GenerateJwtToken(user);

            return token;
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password))) == storedHash;
            }
        }
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        private string GenerateJwtToken(UserMaster user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.UserMasterId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                               signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
