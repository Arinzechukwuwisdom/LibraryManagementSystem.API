using SureLbraryAPI.DTOs;
using SureLbraryAPI.Utilities;

namespace SureLbraryAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<ResponseDetails<GetUserDTO>> CreateUserAsync(CreateUserDTO userDetail);
        Task<ResponseDetails<GetUserDTO>> GetUserByIdAsync(int id);
        Task<ResponseDetails<List<GetUserDTO>>> GetAllUsersAsync();
        Task<ResponseDetails<GetUserDTO>> UpdateUsersAsync(CreateUserDTO userDetail,int id);
        Task<bool> DeleteUserAsync(int id);
    }
}
