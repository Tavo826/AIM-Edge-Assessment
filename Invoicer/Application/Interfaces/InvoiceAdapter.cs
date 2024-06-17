

using Domain.DTOs.Request;
using Domain.DTOs.Response;

namespace Application.Interfaces
{
    public interface InvoiceAdapter
    {
        Task<IEnumerable<Invoice>> GetInvoiceListAsync();
        Task<IEnumerable<Product>> GetProductListByInvoiceIdAsync(int id);
        Task<bool> CreateInvoiceAsync(InvoiceDto invoice);
        Task<bool> UpdateInvoiceAsync(InvoiceDto invoice);
    }
}
