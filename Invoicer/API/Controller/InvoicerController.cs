using Application.Interfaces;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/v1/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class InvoicerController : ControllerBase
    {
        private readonly IServiceGeneric _serviceGeneric;

        public InvoicerController(IServiceGeneric serviceGeneric)
        {
            _serviceGeneric = serviceGeneric;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatusAsync()
        {
            return Ok(await Task.FromResult("Microservice is OK!"));
        }

        [HttpGet]
        public async Task<IActionResult> GetInvoiceList()
        {
            var result = await _serviceGeneric.GetAllInvoices();

            return StatusCode(result.Code, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductListByInvoiceId(int id)
        {
            var result = await _serviceGeneric.GetAllProductsByInvoiceId(id);

            return StatusCode(result.Code, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] InvoiceDto request)
        {

            if (request == null || !ModelState.IsValid)
                return GenerateErrorResponse(StatusCodes.Status400BadRequest, string.Join("|", ModelState.Values.SelectMany(e => e.Errors).Select(c => c.ErrorMessage)));

            var result = await _serviceGeneric.CreateInvoiceAsync(request);

            return StatusCode(result.Code, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] InvoiceDto request)
        {

            if (request == null || !ModelState.IsValid)
                return GenerateErrorResponse(StatusCodes.Status400BadRequest, string.Join("|", ModelState.Values.SelectMany(e => e.Errors).Select(c => c.ErrorMessage)));

            var result = await _serviceGeneric.UpdateInvoiceAsync(request);

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
