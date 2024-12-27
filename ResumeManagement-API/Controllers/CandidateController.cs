using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResumeManagement_API.DTOs;
using ResumeManagement_API.Models;
using ResumeManagement_API.Repositories;
using ResumeManagement_API.Services;
using static ResumeManagement_API.Services.ICandidateServices;

namespace ResumeManagement_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateServices _candidateService;

        public CandidateController(ICandidateServices candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate(AddEditCandidateDto model)
        {
            try
            {
                
                var result = await _candidateService.AddCandidateAsync(model);
              
                var response = new ResponseModel<object>(result, "Candidate added successfully.", 201);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Return error response with 400 Bad Request status code
                var response = new ResponseModel<object>(ex.Message, 400);
                return StatusCode(response.StatusCode, response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditCandidate([FromBody]  AddEditCandidateDto model)
        {
            try
            {
                await _candidateService.EditCandidateAsync(model);
              
                var response = new ResponseModel<object>(null, "Candidate updated successfully.", 201);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Return error response with 400 Bad Request status code
                var response = new ResponseModel<object>(ex.Message, 400);
                return StatusCode(response.StatusCode, response);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> UploadCV( [FromForm]Guid candidateID , IFormFile cvFile)
        {
            try
            {
                await _candidateService.UploadCv(candidateID ,  cvFile);

                var response = new ResponseModel<object>(null, "Candidate updated successfully.", 201);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Return error response with 400 Bad Request status code
                var response = new ResponseModel<object>(ex.Message, 400);
                return StatusCode(response.StatusCode, response);
            }
        }

       
        [HttpDelete]
        public async Task<IActionResult> DeActivateCandidateAsync([FromBody] Candidate model)
        {
            try
            {
                await _candidateService.DeActivateCandidateAsync(model);

                var response = new ResponseModel<object>(null, "Candidate updated successfully.", 201);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Return error response with 400 Bad Request status code
                var response = new ResponseModel<object>(ex.Message, 400);
                return StatusCode(response.StatusCode, response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            try
            {
                var countries = await _candidateService.GetAllCountries();
                var response = new ResponseModel<object>(countries, "Countries fetched", 201);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Return error response with 400 Bad Request status code
                var response = new ResponseModel<object>(ex.Message, 400);
                return StatusCode(response.StatusCode, response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidates()
        {
            try
            {
                var candidates = await _candidateService.GetAllCandidates();
                var response = new ResponseModel<object>(candidates, "Candidates fetched", 201);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Return error response with 400 Bad Request status code
                var response = new ResponseModel<object>(ex.Message, 400);
                return StatusCode(response.StatusCode, response);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCities(int countryID)
        {
            try
            {
                var cities = await _candidateService.GetAllCities(countryID);
                var response = new ResponseModel<object>(cities, "Cities fetched", 201);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Return error response with 400 Bad Request status code
                var response = new ResponseModel<object>(ex.Message, 400);
                return StatusCode(response.StatusCode, response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStatuses()
        {
            try
            {
                var statuses = await _candidateService.GetAllStatus();
                var response = new ResponseModel<object>(statuses, "All status fetched", 201);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Return error response with 400 Bad Request status code
                var response = new ResponseModel<object>(ex.Message, 400);
                return StatusCode(response.StatusCode, response);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCv(Guid candidateId)
        {
            var candidate = await _candidateService.GetCandidateCVByCandidateID(candidateId);

            if (candidate == null || candidate.FileData == null)
            {
                return NotFound("CV not found.");
            }

            var fileBytes = candidate.FileData; // Binary data from the database
            return File(fileBytes, "application/pdf", candidate.FileName); // Assuming PDF file
        }

        [HttpGet]
        public async Task<IActionResult> GetCandidateByCandidateID(Guid candidateId)
        {
            
            try
            {
                var candidate = await _candidateService.GetCandidateByCandidateID(candidateId);
                var response = new ResponseModel<object>(candidate, "Successfully fetched user", 201);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Return error response with 400 Bad Request status code
                var response = new ResponseModel<object>(ex.Message, 400);
                return StatusCode(response.StatusCode, response);
            }
        }
    }

}

