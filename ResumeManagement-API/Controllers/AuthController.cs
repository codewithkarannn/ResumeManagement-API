using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResumeManagement_API.DTOs;
using ResumeManagement_API.Services;


namespace ResumeManagement_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authService;

        public AuthController(IAuthServices authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var responses = new ResponseModel<object>("Validation failed", StatusCodes.Status400BadRequest);
                    return Ok(responses); // Return a BadRequest with the response object
                }
                await _authService.ResgisterUserAsync(dto);
                // Return success response with a message
                var response = new ResponseModel<object>(null, "User registered successfully",201);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Return error response with 400 Bad Request status code
                var response = new ResponseModel<object>(ex.Message, 400);
                return StatusCode(response.StatusCode, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            try
            {
                
                var token = await _authService.LoginUserAsync(dto);
                var response = new ResponseModel<object>(token, "User registered successfully", 201);
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
