using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ResumeManagement_API.DTOs;
using ResumeManagement_API.Models;

namespace ResumeManagement_API.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ResumeManagementContext db;
        private readonly IMapper _mapper;
        public CandidateRepository(ResumeManagementContext _db, IMapper mapper)
        {
            db = _db;
            _mapper = mapper;
        }
        public async Task <CandidateDto> AddCandidateAsync(Candidate model)
        {
            try
            {
                model.CandidateId = Guid.NewGuid();
                model.CreatedAt = System.DateTime.UtcNow;
               var newEntity=  await db.Candidates.AddAsync(model);
                await db.SaveChangesAsync();

                var newModel = _mapper.Map<CandidateDto>(newEntity.Entity);
                return newModel;
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
                var existingFile = await db.CandidateCvfiles.Where(i => i.CandidateId == model.CandidateId && i.IsActive==1).AsNoTracking().FirstOrDefaultAsync();

                if (existingFile != null)
                {
                    existingFile.IsActive = 0;
                    db.CandidateCvfiles.Update(existingFile);

                    model.FileId = Guid.NewGuid();
                    model.CreatedAt = System.DateTime.UtcNow;
                    await db.CandidateCvfiles.AddAsync(model);
                    await db.SaveChangesAsync();
                   
                }
                else
                {
                    model.FileId = Guid.NewGuid();
                    model.CreatedAt = System.DateTime.UtcNow;
                    await db.CandidateCvfiles.AddAsync(model);
                    await db.SaveChangesAsync();
                }
                
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
                    .Where(i => i.CandidateId == candidateID && i.IsActive ==1)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("\"There was an error . Please try again.\"", ex);
            }
        }

        public async Task<CandidateDto>? GetCandidateDtoByCandidateID(Guid candidateID)
        {
            try
            {
                var candidate = await db.Candidates.Include(i => i.Status).Include(i => i.City).Include(i => i.CandidateCvfiles).Select(i => new CandidateDto
                {
                    CandidateId = i.CandidateId,
                    Name = i.Name,
                    Mobile = i.Mobile,
                    Email = i.Email,
                    CityId = i.CityId,
                    InterviewDate = i.InterviewDate ?? null,
                    InterviewFeedback = i.InterviewFeedback ?? null,
                    StatusId = i.StatusId,
                    StatusName = (i.StatusId == null) ? null : i.Status.StatusName,
                    CityName = (i.CityId == null) ? null : i.City.CityName,
                    Ctc = i.Ctc ?? null,
                    ExpectedCtc = i.ExpectedCtc ?? null,
                    IsActive = i.IsActive,
                    CvFileName =  i.CandidateCvfiles.Where(i=>i.CandidateId == candidateID ).Select(i=>i.FileName).FirstOrDefault() ?? null,

                }).Where(i=>i.CandidateId ==  candidateID).FirstOrDefaultAsync();
                return candidate;
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

        public async Task<List<CandidateDto>>? GetAllCandidates()
        {
            try
            {

                var candidates = await db.Candidates.Include(i => i.Status).Include(i => i.City).Include(i => i.CandidateCvfiles).Select(i => new CandidateDto
                {
                    CandidateId = i.CandidateId,
                    Name = i.Name,
                    Mobile = i.Mobile,
                    Email = i.Email,
                    CityId = i.CityId,
                    InterviewDate = i.InterviewDate ?? null,
                    InterviewFeedback =  i.InterviewFeedback  ?? null,
                    StatusId = i.StatusId,
                    StatusName = (i.StatusId == null) ? null : i.Status.StatusName,
                    CityName = (i.CityId == null) ? null : i.City.CityName,
                    Ctc = i.Ctc ?? null,
                    ExpectedCtc = i.ExpectedCtc ?? null,
                    IsActive = i.IsActive,
                    

                }).ToListAsync();
                return candidates;
            }
            catch (Exception ex)
            {
                throw new Exception("\"There was an error . Please try again.\"", ex);
            }
        }

    }
}
