using Domain.DTOs.Request;

namespace Application.Interfaces
{
    public interface UserAdapter
    {
        Task<bool> VerifyUserAsync(UserDto user);
    }
}
