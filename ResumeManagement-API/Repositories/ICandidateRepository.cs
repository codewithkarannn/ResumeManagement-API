using ResumeManagement_API.DTOs;
using ResumeManagement_API.Models;

namespace ResumeManagement_API.Repositories
{
    public interface ICandidateRepository
    {
        // Method to add a new candidate
        Task<CandidateDto> AddCandidateAsync(Candidate model);

        Task AddCandidateCVFIleAsync(CandidateCvfile model);

        Task EditCandidateCVFIleAsync(CandidateCvfile model);

        // Method to edit an existing candidate
        Task EditCandidateAsync(Candidate model);

        // Method to deactivate a candidate
        Task DeActivateCandidateAsync(Candidate model);

        // Method to get all countries
        Task<List<CountryMaster>> GetAllCountries();

        // Method to get all cities based on the country ID
        Task<List<CityMaster>> GetAllCities(int countryID);

        // Method to get all candidate statuses
        Task<List<StatusMaster>> GetAllStatus();

        Task<Candidate> GetCandidateByCandidateID(Guid candidateID);

        Task<CandidateDto>? GetCandidateDtoByCandidateID(Guid candidateID);

        Task<CandidateCvfile> GetCandidateCVByCandidateID(Guid candidateID);
        Task<Candidate> GetCandidateByCandidateEmail(string email);
        Task<List<CandidateDto>>? GetAllCandidates();
        

    }
}
