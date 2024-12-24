using ResumeManagement_API.Models;

namespace ResumeManagement_API.Repositories
{
    public interface IUserRepository
    {
        Task<UserMaster> GetUserByEmailAsync(string email);
        Task AddUserAsync(UserMaster user);
        Task<bool> IsEmailExistsAsync( string email);
    }
}
