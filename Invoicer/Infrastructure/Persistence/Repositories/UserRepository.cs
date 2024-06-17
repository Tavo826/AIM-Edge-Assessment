using Domain.DTOs.Request;
using Infrastructure.Persistence.Mappers.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public interface UserRepository
    {
        Task<UserEntity> GetUserByUsername(UserDto user);
    }
}
