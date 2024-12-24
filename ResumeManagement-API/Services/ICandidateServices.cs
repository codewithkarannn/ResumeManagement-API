
using ResumeManagement_API.DTOs;
using ResumeManagement_API.Models;

namespace ResumeManagement_API.Services
{
    public interface ICandidateServices
    {

        // Add a new candidate
        Task AddCandidateAsync(AddEditCandidateDto model);

        // Edit an existing candidate
        Task EditCandidateAsync(AddEditCandidateDto model);

        // Deactivate a candidate
        Task DeActivateCandidateAsync(Candidate model);

        // Get all countries
        Task<List<CountryDto>> GetAllCountries();

        // Get all cities based on country ID
        Task<List<CityDto>> GetAllCities(int countryID);

        // Get all statuses
        Task<List<StatusDto>> GetAllStatus();
        Task<Candidate> GetCandidateByCandidateID(Guid candidateID);

        Task<CandidateCvfile> GetCandidateCVByCandidateID(Guid candidateID);
    }
}
