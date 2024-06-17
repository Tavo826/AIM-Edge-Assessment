

using Domain.DTOs.Request;
using Infrastructure.Persistence.Mappers.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public interface InvoiceRepository
    {
        Task<IEnumerable<InvoiceEntity>> GetAllInvoices();
        Task<IEnumerable<ProductEntity>> GetAllProductsByInvoiceId(int id);
        Task<bool> SaveInvoice(InvoiceDto invoice);
        Task<bool> UpdateInvoice(InvoiceDto invoice);
    }
}
