using Application.Interfaces;
using Domain.DTOs.Request;
using Domain.DTOs.Response;

namespace Application.Implementations
{
    public class ServiceGeneric : IServiceGeneric
    {
        private readonly InvoiceAdapter _invoiceAdapter;
        private readonly UserAdapter _userAdapter;

        public ServiceGeneric(InvoiceAdapter invoiceAdapter, UserAdapter userAdapter)
        {
            _invoiceAdapter = invoiceAdapter;
            _userAdapter = userAdapter;
        }

        public async Task<InvoiceResponse<Invoice>> GetAllInvoices()
        {
            var result = await _invoiceAdapter.GetInvoiceListAsync();

            return GenerateInvoiceResponse(result);
        }

        public async Task<InvoiceResponse<Product>> GetAllProductsByInvoiceId(int id)
        {
            var result = await _invoiceAdapter.GetProductListByInvoiceIdAsync(id);

            return GenerateInvoiceResponse(result);
        }

        public async Task<GenericResponse> CreateInvoiceAsync(InvoiceDto requestDto)
        {
            try
            {
                var result = await _invoiceAdapter.CreateInvoiceAsync(requestDto);

                return GenerateGenericResponse(result, "Success", 200);
            }
            catch (Exception ex) 
            {
                return GenerateGenericResponse(false, ex.Message, 500);
            }          

            
        }

        public async Task<GenericResponse> UpdateInvoiceAsync(InvoiceDto requestDto)
        {
            try
            {
                var result = await _invoiceAdapter.UpdateInvoiceAsync(requestDto);

                return GenerateGenericResponse(result, "Success", 200);
            }
            catch (Exception ex)
            {
                return GenerateGenericResponse(false, ex.Message, 500);
            }
        }

        public async Task<GenericResponse> ValidateUserAsync(UserDto userDto)
        {
            var result = await _userAdapter.VerifyUserAsync(userDto);

            if (result)
                return GenerateGenericResponse(result, "Success", 200);

            return GenerateGenericResponse(result, "Failed", 404);
        }

        private GenericResponse GenerateGenericResponse(bool result, string message, int code)
        {
            return new GenericResponse()
            {
                Response = result,
                Message = message,
                Code = code
            };
        }

        private InvoiceResponse<T> GenerateInvoiceResponse<T>(IEnumerable<T> result)
        {
            return new InvoiceResponse<T>()
            {
                Details = result,
                Response = true,
                Message = "Success",
                Code = 200
            };
        }
    }
}