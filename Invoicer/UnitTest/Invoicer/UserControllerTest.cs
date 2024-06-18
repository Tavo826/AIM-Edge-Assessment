using API.Controller;
using Application.Interfaces;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Moq;

namespace UnitTest.Invoicer
{
    public class UserControllerTest
    {

        private static readonly string STR_TEST = "Success";
        private static readonly bool BOOL_TEST = true;
        private static readonly int INT_TEST = 200;

        [Fact]
        public async Task ValidateAsync_RequestIsProvided_Response()
        {
            Mock<IServiceGeneric> _mockServiceGeneric = new Mock<IServiceGeneric>();

            UserDto userDto = GetUserDto();
            GenericResponse response = GetResponse();

            UserController userController = GetUserControllerTooInstance(_mockServiceGeneric.Object);

            SetUpValidateUserAsyncMethod(_mockServiceGeneric, response);

            var actual = userController.ValidateAsync(userDto);

            Assert.NotNull(actual);
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

        private void SetUpValidateUserAsyncMethod(Mock<IServiceGeneric> serviceGeneric, GenericResponse response)
        {
            serviceGeneric.Setup(x => x.ValidateUserAsync(It.IsAny<UserDto>())).ReturnsAsync(response);
        }

        private UserController GetUserControllerTooInstance(IServiceGeneric serviceGeneric)
        {
            return new UserController(serviceGeneric);
        }

        private UserDto GetUserDto()
        {
            return new UserDto
            {
                Username = STR_TEST,
                Password = STR_TEST
            };
        }
    }
}
