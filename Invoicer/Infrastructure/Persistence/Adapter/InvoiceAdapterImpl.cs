using Application.Interfaces;
using AutoMapper;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence.Adapter
{
    public class InvoiceAdapterImpl : InvoiceAdapter
    {
        private readonly InvoiceRepository _repository;
        private readonly IMapper _mapper;

        public InvoiceAdapterImpl(InvoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CreateInvoiceAsync(InvoiceDto invoice)
        {
            invoice.Total = invoice.Subtotal - (invoice.Subtotal * (invoice.Discount / 100));
            return await _repository.SaveInvoice(invoice);
        }

        public async Task<IEnumerable<Invoice>> GetInvoiceListAsync()
        {
            var result = await _repository.GetAllInvoices();

            return _mapper.Map<IEnumerable<Invoice>>(result);
        }

        public async Task<IEnumerable<Product>> GetProductListByInvoiceIdAsync(int id)
        {
            var result = await _repository.GetAllProductsByInvoiceId(id);

            return _mapper.Map<IEnumerable<Product>>(result);
        }

        public Task<bool> UpdateInvoiceAsync(InvoiceDto invoice)
        {
            return _repository.UpdateInvoice(invoice);
        }
    }
}
