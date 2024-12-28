using AutoMapper;
using ResumeManagement_API.DTOs;
using ResumeManagement_API.Models;
using ResumeManagement_API.Repositories;

namespace ResumeManagement_API.Services
{
    public class CandidateService : ICandidateServices
    {

        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository candidateRepository, IMapper  mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }

        public async Task <CandidateDto> AddCandidateAsync(AddEditCandidateDto model)
        {
            try
            {
                var existingModel = await _candidateRepository.GetCandidateByCandidateEmail(model.Email);
                if(existingModel != null) 
                {

                    throw new KeyNotFoundException($"User with '{model.Email}' already found.");
                }
                else
                {

                    var newModel = _mapper.Map<Candidate>(model);
                    newModel.CandidateId = Guid.NewGuid();
                    newModel.IsActive = 1;
                    newModel.CreatedAt = System.DateTime.UtcNow;

                    return  await _candidateRepository.AddCandidateAsync(newModel);

                }
               
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding the candidate. Please try again.", ex);
            }
        }

        public async Task EditCandidateAsync(AddEditCandidateDto model)
        {
            try
            {
                var existingModel = await _candidateRepository.GetCandidateByCandidateID(model.CandidateId ?? Guid.Empty);
                if(existingModel != null)
                {
                    var editModel = _mapper.Map<Candidate>(model);
                  
                    await _candidateRepository.EditCandidateAsync(editModel);
                }
                else

                {
                    throw new KeyNotFoundException($"'{model.Name}' not found.");
                }

                
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while editing the candidate. Please try again.", ex);
            }
        }

        public async Task DeActivateCandidateAsync(Guid candidateID)
        {
            try
            {
                await _candidateRepository.DeActivateCandidateAsync(candidateID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deactivating the candidate. Please try again.", ex);
            }
        }

        public async Task<List<CountryDto>> GetAllCountries()
        {
            try
            {
                return  _mapper.Map<List<CountryDto>>(await _candidateRepository.GetAllCountries());
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching the list of countries. Please try again.", ex);
            }
        }

        public async Task<List<CityDto>> GetAllCities(int countryID)
        {
            try
            {
                return _mapper.Map<List<CityDto>>(await _candidateRepository.GetAllCities(countryID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching the list of cities. Please try again.", ex);
            }
        }

        public async Task<List<StatusDto>> GetAllStatus()
        {
            try
            {
                return _mapper.Map<List<StatusDto>>( await _candidateRepository.GetAllStatus());
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching the list of statuses. Please try again.", ex);
            }
        }

        public async Task<CandidateDto>? GetCandidateByCandidateID(Guid candidateID)
        {
            try
            {
                return await _candidateRepository.GetCandidateDtoByCandidateID(candidateID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching candidate. Please try again.", ex);
            }
        }

        public async Task<CandidateCvfile> GetCandidateCVByCandidateID(Guid candidateID)
        {
            try
            {
                return await _candidateRepository.GetCandidateCVByCandidateID(candidateID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching candidate. Please try again.", ex);
            }
        }


        public async Task<List<CandidateDto>> GetAllCandidates()
        {
            try
            {
                return await _candidateRepository.GetAllCandidates();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching candidate. Please try again.", ex);
            }
        }


        public async Task UploadCv( Guid  candidateID , IFormFile CvFile)
        {
            try
            {
                var existingModel = await _candidateRepository.GetCandidateByCandidateID(candidateID );
                if (existingModel != null)
                {

                    if (CvFile != null)
                    {
                        // Validate the file type and size
                        var fileExtension = Path.GetExtension(CvFile.FileName).ToLower();
                        if (fileExtension != ".pdf")
                        {
                            throw new ArgumentException("The CV must be a PDF file.");
                        }

                        using (var memoryStream = new MemoryStream())
                        {
                            await CvFile.CopyToAsync(memoryStream);

                            var newCv = new CandidateCvfile
                            {
                                FileId = Guid.NewGuid(),
                                FileData = memoryStream.ToArray(),
                                FileName = CvFile.FileName,
                                FileType = fileExtension,
                                FileSize = CvFile.Length,
                                CandidateId = candidateID,
                            };
                            await _candidateRepository.AddCandidateCVFIleAsync(newCv);
                        }

                    }
                    
                }
                else

                {
                    throw new KeyNotFoundException($" User  not found.");
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while editing the candidate. Please try again.", ex);
            }
        }


    
    }
}
