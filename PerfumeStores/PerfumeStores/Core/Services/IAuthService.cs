using PerfumeStores.Core.DTOs;
using PerfumeStores.Core.Models;

namespace PerfumeStores.Core.Services
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterDTO registerDTO);
        Task<Customer?> Login(LoginDTO loginDTO);
    }
}
