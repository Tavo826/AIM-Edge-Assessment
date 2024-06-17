using Application.Interfaces;
using Domain.DTOs.Request;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence.Adapter
{
    public class UserAdapterImpl : UserAdapter
    {
        private readonly UserRepository _userRepository;

        public UserAdapterImpl(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> VerifyUserAsync(UserDto user)
        {
            var result = await _userRepository.GetUserByUsername(user);

            if (result == null || result.Password != user.Password)
                return false;

            return true;
            
        }
    }
}
