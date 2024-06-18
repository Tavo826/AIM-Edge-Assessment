using Application.Implementations;
using Application.Interfaces;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Moq;

namespace UnitTest.Application
{
    public class ServiceGenericTest
    {
        private static readonly int INT_TEST = 200;
        private static readonly string STR_TEST = "Success";
        private static readonly float FLOAT_TEST = 100;
        private static readonly bool BOOL_TEST = true;


        [Fact]
        public async Task GetAllInvoices_ResponseInvoice()
        {
            Mock<InvoiceAdapter> _mockInvoiceAdapter = new Mock<InvoiceAdapter>();
            Mock<UserAdapter> _mockUserAdapter = new Mock<UserAdapter>();

            Invoice invoice = GetInvoice();
            InvoiceResponse<Invoice> response = GetInvoiceResponse(invoice);

            SetUpGetAllInvoicesAsyncMethod(_mockInvoiceAdapter, invoice);

            ServiceGeneric serviceGeneric = new ServiceGeneric(_mockInvoiceAdapter.Object, _mockUserAdapter.Object);

            var actual = await serviceGeneric.GetAllInvoices();

            Assert.NotNull(actual);
            Assert.Equal(response.Response, actual.Response);
        }

        [Fact]
        public async Task GetAllProductByInvoiceId_ResponseProduct()
        {
            Mock<InvoiceAdapter> _mockInvoiceAdapter = new Mock<InvoiceAdapter>();
            Mock<UserAdapter> _mockUserAdapter = new Mock<UserAdapter>();

            Product product = GetProduct();
            InvoiceResponse<Product> response = GetInvoiceResponse(product);

            SetUpGetAllProductsAsyncMethod(_mockInvoiceAdapter, product);

            ServiceGeneric serviceGeneric = new ServiceGeneric(_mockInvoiceAdapter.Object, _mockUserAdapter.Object);

            var actual = await serviceGeneric.GetAllProductsByInvoiceId(It.IsAny<int>());

            Assert.NotNull(actual);
            Assert.Equal(response.Response, actual.Response);
        }

        [Fact]
        public async Task CreateInvoice_ResponseGeneric()
        {
            Mock<InvoiceAdapter> _mockInvoiceAdapter = new Mock<InvoiceAdapter>();
            Mock<UserAdapter> _mockUserAdapter = new Mock<UserAdapter>();

            InvoiceDto invoiceDto = GetInvoiceDto();

            SetUpCreateInvoiceAsyncMethod(_mockInvoiceAdapter);

            ServiceGeneric serviceGeneric = new ServiceGeneric(_mockInvoiceAdapter.Object, _mockUserAdapter.Object);

            var actual = await serviceGeneric.CreateInvoiceAsync(invoiceDto);

            Assert.NotNull(actual);
            Assert.True(actual.Response);
        }

        [Fact]
        public async Task UpdateInvoice_ResponseGeneric()
        {
            Mock<InvoiceAdapter> _mockInvoiceAdapter = new Mock<InvoiceAdapter>();
            Mock<UserAdapter> _mockUserAdapter = new Mock<UserAdapter>();

            InvoiceDto invoiceDto = GetInvoiceDto();

            SetUpUpdateInvoiceAsyncMethod(_mockInvoiceAdapter);

            ServiceGeneric serviceGeneric = new ServiceGeneric(_mockInvoiceAdapter.Object, _mockUserAdapter.Object);

            var actual = await serviceGeneric.UpdateInvoiceAsync(invoiceDto);

            Assert.NotNull(actual);
            Assert.True(actual.Response);
        }

        [Fact]
        public async Task ValidateUser_ResponseGeneric()
        {
            Mock<InvoiceAdapter> _mockInvoiceAdapter = new Mock<InvoiceAdapter>();
            Mock<UserAdapter> _mockUserAdapter = new Mock<UserAdapter>();

            UserDto userDto = GetUserDto();

            SetUpValidateUserAsyncMethod(_mockUserAdapter);

            ServiceGeneric serviceGeneric = new ServiceGeneric(_mockInvoiceAdapter.Object, _mockUserAdapter.Object);

            var actual = await serviceGeneric.ValidateUserAsync(userDto);

            Assert.NotNull(actual);
            Assert.True(actual.Response);
        }

        private void SetUpValidateUserAsyncMethod(Mock<UserAdapter> mockUserAdapter)
        {
            mockUserAdapter.Setup(x => x.VerifyUserAsync(It.IsAny<UserDto>())).ReturnsAsync(true);
        }

        private UserDto GetUserDto()
        {
            return new UserDto
            {
                Username = STR_TEST,
                Password = STR_TEST
            };
        }

        private void SetUpUpdateInvoiceAsyncMethod(Mock<InvoiceAdapter> mockInvoiceAdapter)
        {
            mockInvoiceAdapter.Setup(x => x.UpdateInvoiceAsync(It.IsAny<InvoiceDto>())).ReturnsAsync(true);
        }

        private void SetUpCreateInvoiceAsyncMethod(Mock<InvoiceAdapter> mockInvoiceAdapter)
        {
            mockInvoiceAdapter.Setup(x => x.CreateInvoiceAsync(It.IsAny<InvoiceDto>())).ReturnsAsync(true);
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

        private void SetUpGetAllProductsAsyncMethod(Mock<InvoiceAdapter> mockInvoiceAdapter, Product product)
        {
            mockInvoiceAdapter.Setup(x => x.GetProductListByInvoiceIdAsync(It.IsAny<int>())).ReturnsAsync([product]);
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

        private void SetUpGetAllInvoicesAsyncMethod(Mock<InvoiceAdapter> mockInvoiceAdapter, Invoice response)
        {
            mockInvoiceAdapter.Setup(x => x.GetInvoiceListAsync()).ReturnsAsync([response]);
        }
    }
}
