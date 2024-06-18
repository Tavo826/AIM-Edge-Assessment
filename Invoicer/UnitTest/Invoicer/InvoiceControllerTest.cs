using API.Controller;
using Application.Interfaces;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Moq;

namespace UnitTest.Invoicer
{
    public class InvoiceControllerTest
    {
        private static readonly int INT_TEST = 200;
        private static readonly string STR_TEST = "Success";
        private static readonly float FLOAT_TEST = 100;
        private static readonly bool BOOL_TEST = true;

        [Fact]
        public async Task GetInvoiceList_Response()
        {
            Mock<IServiceGeneric> _mockServiceGeneric = new Mock<IServiceGeneric>();

            Invoice invoice = GetInvoice();
            InvoiceResponse<Invoice> response = GetInvoiceResponse(invoice);

            InvoicerController invoicerController = GetInvoicerControllerToolInstance(_mockServiceGeneric.Object);

            SetUpGetAllInvoicesAsyncMethod(_mockServiceGeneric, response);

            var actual = invoicerController.GetInvoiceList();

            Assert.NotNull(actual);
        }

        [Fact]
        public async Task GetProductListByInvoiceId_ResponseProduct()
        {
            Mock<IServiceGeneric> _mockServiceGeneric = new Mock<IServiceGeneric>();

            Product product = GetProduct();
            InvoiceResponse<Product> response = GetInvoiceResponse(product);

            SetUpGetAllProductsAsyncMethod(_mockServiceGeneric, response);

            InvoicerController invoicerController = GetInvoicerControllerToolInstance(_mockServiceGeneric.Object);

            var actual = await invoicerController.GetProductListByInvoiceId(It.IsAny<int>());

            Assert.NotNull(actual);
        }

        [Fact]
        public async Task Create_ResquestIsGiven_ResponseProduct()
        {
            Mock<IServiceGeneric> _mockServiceGeneric = new Mock<IServiceGeneric>();

            InvoiceDto invoiceDto = GetInvoiceDto();
            GenericResponse response = GetResponse();

            SetUpCreateInvoiceAsyncMethod(_mockServiceGeneric, response);

            InvoicerController invoicerController = GetInvoicerControllerToolInstance(_mockServiceGeneric.Object);

            var actual = await invoicerController.CreateAsync(invoiceDto);

            Assert.NotNull(actual);
        }

        [Fact]
        public async Task Update_ResquestIsGiven_ResponseProduct()
        {
            Mock<IServiceGeneric> _mockServiceGeneric = new Mock<IServiceGeneric>();

            InvoiceDto invoiceDto = GetInvoiceDto();
            GenericResponse response = GetResponse();

            SetUpUpdateInvoiceAsyncMethod(_mockServiceGeneric, response);

            InvoicerController invoicerController = GetInvoicerControllerToolInstance(_mockServiceGeneric.Object);

            var actual = await invoicerController.UpdateAsync(invoiceDto);

            Assert.NotNull(actual);
        }

        private void SetUpUpdateInvoiceAsyncMethod(Mock<IServiceGeneric> mockServiceGeneric, GenericResponse response)
        {
            mockServiceGeneric.Setup(x => x.UpdateInvoiceAsync(It.IsAny<InvoiceDto>())).ReturnsAsync(response);
        }

        private GenericResponse GetResponse()
        {
            return new GenericResponse
            {
                Response = BOOL_TEST,
                Message = STR_TEST,
                Code = INT_TEST
            };
        }

        private void SetUpCreateInvoiceAsyncMethod(Mock<IServiceGeneric> mockServiceGeneric, GenericResponse response)
        {
            mockServiceGeneric.Setup(x => x.CreateInvoiceAsync(It.IsAny<InvoiceDto>())).ReturnsAsync(response);
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

        private void SetUpGetAllProductsAsyncMethod(Mock<IServiceGeneric> mockServiceGeneric, InvoiceResponse<Product> response)
        {
            mockServiceGeneric.Setup(x => x.GetAllProductsByInvoiceId(It.IsAny<int>())).ReturnsAsync(response);
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

        private void SetUpGetAllInvoicesAsyncMethod(Mock<IServiceGeneric> mockServiceGeneric, InvoiceResponse<Invoice> response)
        {
            mockServiceGeneric.Setup(x => x.GetAllInvoices()).ReturnsAsync(response);
        }

        private InvoicerController GetInvoicerControllerToolInstance(IServiceGeneric serviceGeneric)
        {
            return new InvoicerController(serviceGeneric);
        }

        private InvoiceResponse<T> GetInvoiceResponse<T>(T value)
        {
            return new InvoiceResponse<T>
            {
                Details = [value],
                Response = BOOL_TEST,
                Message = STR_TEST,
                Code = INT_TEST
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
                Total = FLOAT_TEST,
            };
        }
    }
}
