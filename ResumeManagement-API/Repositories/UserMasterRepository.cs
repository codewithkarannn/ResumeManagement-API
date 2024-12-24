using Microsoft.EntityFrameworkCore;
using ResumeManagement_API.Models;

namespace ResumeManagement_API.Repositories
{
    public class UserMasterRepository : IUserRepository
    {

        private readonly ResumeManagementContext db;

        public UserMasterRepository(ResumeManagementContext _db)
        {
            db = _db;
        }
        public async Task AddUserAsync(UserMaster user)
        {
            try
            {
                user.UserMasterId = Guid.NewGuid();
                user.CreatedAt =  System.DateTime.UtcNow;
                await  db.UserMasters.AddAsync(user);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("\"There was an error add new user. Please try again.\"" , ex);
            }
        }
            
        public async Task<UserMaster> GetUserByEmailAsync(string email)
        {
            try
            {
                return await db.UserMasters.Where(i => i.Email == email).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("\"There was an error getting user. Please try again.\"", ex);
            }
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            try
            {
                var exists = await db.UserMasters.AnyAsync(u => u.Email == email );
                return exists;
            }
            catch (Exception ex)
            {

                throw new Exception("\"There was an error getting user. Please try again.\"", ex);
            }
        }
    }
}
