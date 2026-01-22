using SureLbraryAPI.DTOs;
using SureLbraryAPI.Utilities;

namespace SureLbraryAPI.Interfaces
{
    public interface IAuthRepository
    {
        Task<ResponseDetails<GetUserDTO>> RegisterAsync(CreateUserDTO request);
        Task<ResponseDetails<ResponseLoginDTO>> LoginUserAsync(LoginUserDTO request);
    }
}
