using AutoMapper;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Infrastructure.Persistence.Adapter;
using Infrastructure.Persistence.Mappers.Entities;
using Infrastructure.Persistence.Repositories;
using Moq;

namespace UnitTest.Persistence
{
    public class InvoiceAdapterImplTest
    {

        private static readonly string STR_TEST = "Success";
        private static readonly float FLOAT_TEST = 100;
        private static readonly int INT_TEST = 200;

        [Fact]
        public async Task CreateInvoice_RequestIsGiven_Response()
        {
            Mock<InvoiceRepository> _mockInvoiceRepository = new Mock<InvoiceRepository>();
            Mock<IMapper> _mockMapper = new Mock<IMapper>();

            InvoiceDto invoiceDto = GetInvoiceDto();

            InvoiceAdapterImpl invoiceAdapter = new InvoiceAdapterImpl(_mockInvoiceRepository.Object, _mockMapper.Object);

            _mockInvoiceRepository.Setup(x => x.SaveInvoice(It.IsAny<InvoiceDto>())).ReturnsAsync(true);

            var actual = await invoiceAdapter.CreateInvoiceAsync(invoiceDto);

            Assert.True(actual);
        }

        [Fact]
        public async Task GetInvoiceList_RequestIsGiven_Response()
        {
            Mock<InvoiceRepository> _mockInvoiceRepository = new Mock<InvoiceRepository>();
            Mock<IMapper> _mockMapper = new Mock<IMapper>();

            Invoice invoice = GetInvoice();
            InvoiceEntity invoiceEntity = GetInvoiceEntity();

            InvoiceAdapterImpl invoiceAdapter = new InvoiceAdapterImpl(_mockInvoiceRepository.Object, _mockMapper.Object);

            _mockInvoiceRepository.Setup(x => x.GetAllInvoices()).ReturnsAsync([invoiceEntity]);
            _mockMapper.Setup(x => x.Map<IEnumerable<Invoice>>(It.IsAny<IEnumerable<InvoiceEntity>>())).Returns([invoice]);

            var actual = await invoiceAdapter.GetInvoiceListAsync();

            Assert.NotNull(actual);
            Assert.Equal(invoice.Id, actual.First().Id);
        }

        [Fact]
        public async Task GetProductListByInvoiceId_RequestIsGiven_Response()
        {
            Mock<InvoiceRepository> _mockInvoiceRepository = new Mock<InvoiceRepository>();
            Mock<IMapper> _mockMapper = new Mock<IMapper>();

            Product product = GetProduct();
            ProductEntity productEntity = GetProductEntity();

            InvoiceAdapterImpl invoiceAdapter = new InvoiceAdapterImpl(_mockInvoiceRepository.Object, _mockMapper.Object);

            _mockInvoiceRepository.Setup(x => x.GetAllProductsByInvoiceId(It.IsAny<int>())).ReturnsAsync([productEntity]);
            _mockMapper.Setup(x => x.Map<IEnumerable<Product>>(It.IsAny<IEnumerable<ProductEntity>>())).Returns([product]);

            var actual = await invoiceAdapter.GetProductListByInvoiceIdAsync(It.IsAny<int>());

            Assert.NotNull(actual);
            Assert.Equal(product.Id, actual.First().Id);
        }

        [Fact]
        public async Task UpdateInvoice_RequestIsGiven_Response()
        {
            Mock<InvoiceRepository> _mockInvoiceRepository = new Mock<InvoiceRepository>();
            Mock<IMapper> _mockMapper = new Mock<IMapper>();

            InvoiceDto invoiceDto = GetInvoiceDto();

            InvoiceAdapterImpl invoiceAdapter = new InvoiceAdapterImpl(_mockInvoiceRepository.Object, _mockMapper.Object);

            _mockInvoiceRepository.Setup(x => x.UpdateInvoice(It.IsAny<InvoiceDto>())).ReturnsAsync(true);

            var actual = await invoiceAdapter.UpdateInvoiceAsync(invoiceDto);

            Assert.True(actual);
        }

        private ProductEntity GetProductEntity()
        {
            return new ProductEntity
            {
                Id = INT_TEST,
                Product_id = INT_TEST,
                Quantity = INT_TEST,
                Description = STR_TEST,
                Invoice_id = INT_TEST
            };
        }

        private Product GetProduct()
        {
            return new Product
            {
                Id = INT_TEST,
                Product_id = INT_TEST,
                Quantity = INT_TEST,
                Description = STR_TEST,
                Invoice_id = INT_TEST
            };
        }

        private InvoiceEntity GetInvoiceEntity()
        {
            return new InvoiceEntity
            {
                Id = INT_TEST,
                Client = STR_TEST,
                Date = DateTime.MinValue,
                Subtotal = FLOAT_TEST,
                Discount = FLOAT_TEST,
                Total = FLOAT_TEST
            };
        }

        private Invoice GetInvoice()
        {
            return new Invoice
            {
                Id = INT_TEST,
                Client = STR_TEST,
                Date = DateTime.MinValue,
                Subtotal = FLOAT_TEST,
                Discount = FLOAT_TEST,
                Total = FLOAT_TEST
            };
        }

        private InvoiceDto GetInvoiceDto()
        {
            return new InvoiceDto
            {
                Client = STR_TEST,
                Date = DateTime.MinValue,
                Subtotal = FLOAT_TEST,
                Discount = FLOAT_TEST,
                Total = FLOAT_TEST
            };
        }
    }
}
