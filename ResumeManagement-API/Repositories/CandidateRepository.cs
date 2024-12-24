using Microsoft.EntityFrameworkCore;
using ResumeManagement_API.Models;

namespace ResumeManagement_API.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ResumeManagementContext db;
        public CandidateRepository(ResumeManagementContext _db)
        {
            db = _db;
        }
        public async Task AddCandidateAsync(Candidate model)
        {
            try
            {
                model.CandidateId = Guid.NewGuid();
                model.CreatedAt = System.DateTime.UtcNow;
                await db.Candidates.AddAsync(model);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("\"There was an error add new candidate. Please try again.\"", ex);
            }
        }

        public async Task AddCandidateCVFIleAsync(CandidateCvfile model)
        {
            try
            {
                model.FileId = Guid.NewGuid();
                model.CreatedAt = System.DateTime.UtcNow;
                await db.CandidateCvfiles.AddAsync(model);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("\"There was an error add  candidate new cv file. Please try again.\"", ex);
            }
        }

        public async Task EditCandidateCVFIleAsync(CandidateCvfile model)
        {
            try
            {
                db.CandidateCvfiles.Update(model);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("\"There was an error edtiting candidate cv file. Please try again.\"", ex);
            }
        }


        public async Task EditCandidateAsync(Candidate model)
        {
            try
            {
                db.Candidates.Update(model);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("\"There was an error edit candidate. Please try again.\"", ex);
            }
        }

        public async Task DeActivateCandidateAsync(Candidate model)
        {
            try
            {
                
                db.Candidates.Update(model);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("\"There was an error editing candidate. Please try again.\"", ex);
            }
        }

        public async  Task< List<CountryMaster>> GetAllCountries()
        {
            try
            {
                return db.CountryMasters.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("\"There was an error . Please try again.\"", ex);
            }
        }

        public async Task<List<CityMaster>> GetAllCities(int countryID)
        {
            try
            {
                return db.CityMasters.Where(i=>i.CountryId == countryID).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("\"There was an error . Please try again.\"", ex);
            }
        }

        public async Task<List<StatusMaster>> GetAllStatus()
        {
            try
            {
                return db.StatusMasters.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("\"There was an error . Please try again.\"", ex);
            }
        }
        public async Task<Candidate> GetCandidateByCandidateID(Guid candidateID)
        {
            try
            {
               return await db.Candidates
                   .Where(i => i.CandidateId == candidateID)
                   .AsNoTracking()
                   .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("\"There was an error . Please try again.\"", ex);
            }
        }

        public async Task<CandidateCvfile> GetCandidateCVByCandidateID(Guid candidateID)
        {
            try
            {
                return await db.CandidateCvfiles
                    .Where(i => i.CandidateId == candidateID)
                    .AsNoTracking().OrderByDescending(i=>i.CreatedAt)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("\"There was an error . Please try again.\"", ex);
            }
        }

        public async Task<Candidate> GetCandidateByCandidateEmail(string email)
        {
            try
            {
                return await db.Candidates
                    .Where(i => i.Email == email)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("\"There was an error . Please try again.\"", ex);
            }
        }
    }
}
