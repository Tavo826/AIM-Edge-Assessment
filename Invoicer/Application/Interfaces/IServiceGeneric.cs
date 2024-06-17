using Domain.DTOs.Request;
using Domain.DTOs.Response;

namespace Application.Interfaces
{
    public interface IServiceGeneric
    {
        Task<InvoiceResponse<Invoice>> GetAllInvoices();
        Task<InvoiceResponse<Product>> GetAllProductsByInvoiceId(int id);
        Task<GenericResponse> CreateInvoiceAsync(InvoiceDto requestDto);
        Task<GenericResponse> UpdateInvoiceAsync(InvoiceDto requestDto);
        Task<GenericResponse> ValidateUserAsync(UserDto userDto);
    }
}
