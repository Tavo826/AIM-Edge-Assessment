using Application.Interfaces;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/v1/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IServiceGeneric _serviceGeneric;

        public UserController(IServiceGeneric serviceGeneric)
        {
            _serviceGeneric = serviceGeneric;
        }

        [HttpPost]
        public async Task<IActionResult> ValidateAsync([FromBody] UserDto userDto)
        {
            if (userDto == null || !ModelState.IsValid)
                return GenerateErrorResponse(StatusCodes.Status400BadRequest, string.Join("|", ModelState.Values.SelectMany(e => e.Errors).Select(c => c.ErrorMessage)));

            var result = await _serviceGeneric.ValidateUserAsync(userDto);

            return StatusCode(result.Code, result);

        }

        private ObjectResult GenerateErrorResponse(int status, string message)
        {
            return StatusCode(status, new GenericResponse
            {
                Response = false,
                Message = message,
                Code = status
            });
        }
    }
}
