using Domain.DTOs.Request;
using Infrastructure.Persistence.Adapter;
using Infrastructure.Persistence.Mappers.Entities;
using Infrastructure.Persistence.Repositories;
using Moq;

namespace UnitTest.Persistence
{
    public class UserAdapterImplTest
    {

        private static readonly string STR_TEST = "Success";

        [Fact]
        public async Task VerifyUser_RequestIsGiven_Response()
        {
            Mock<UserRepository> _mockUserRepository = new Mock<UserRepository>();

            UserEntity userEntity = GetUserEntity();
            UserDto userDto = GetUserDto();

            UserAdapterImpl userAdapter = new UserAdapterImpl(_mockUserRepository.Object);

            SetUpVerifyUserAsyncMethod(_mockUserRepository, userEntity);

            var actual = await userAdapter.VerifyUserAsync(userDto);

            Assert.True(actual);
        }

        private UserDto GetUserDto()
        {
            return new UserDto
            {
                Username = STR_TEST,
                Password = STR_TEST
            };
        }

        private void SetUpVerifyUserAsyncMethod(Mock<UserRepository> userRepository, UserEntity userEntity)
        {
            userRepository.Setup(x => x.GetUserByUsername(It.IsAny<UserDto>())).ReturnsAsync(userEntity);
        }

        private UserEntity GetUserEntity()
        {
            return new UserEntity
            {
                Username = STR_TEST,
                Password = STR_TEST
            };
        }
    }
}
