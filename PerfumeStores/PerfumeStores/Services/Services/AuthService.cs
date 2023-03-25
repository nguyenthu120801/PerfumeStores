using Common.Securities;
using Microsoft.EntityFrameworkCore;
using PerfumeStores.Core.DTOs;
using PerfumeStores.Core.Models;
using PerfumeStores.Core.Services;

namespace PerfumeStores.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly Prn221Context _context;

        public AuthService(Prn221Context context)
        {
            _context = context;
        }

        public async Task<bool> Register(RegisterDTO registerDTO)
        {
            var userExist = await _context.Customers.FirstOrDefaultAsync(x => x.Username.Equals(registerDTO.Username));
            if (userExist != null)
            {
                return false;
            }
            var hashSalt = Cryptography.MD5Hash(registerDTO.Password);
            Customer customer = new Customer()
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Password = hashSalt,
                IsAdmin = false
            };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Customer?> Login(LoginDTO loginDTO)
        {
     
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Username.Equals(loginDTO.Username));
            if (customer != null)
            {
                if (Cryptography.VerifyPassword(loginDTO.Password, customer.Password))
                {
                    return customer;
                }
                else return null;
            }
            else return null;
        }
    }
}
