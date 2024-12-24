using ResumeManagement_API.DTOs;

namespace ResumeManagement_API.Services
{
    public interface IAuthServices
    {
        Task ResgisterUserAsync(RegisterUserDto registerUserDto);
        Task<string> LoginUserAsync(LoginUserDto loginUserDto);
    }
}
