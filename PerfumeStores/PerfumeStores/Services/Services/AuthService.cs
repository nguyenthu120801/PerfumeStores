﻿using AutoMapper;
using Common.Securities;
using Microsoft.EntityFrameworkCore;
using PerfumeStores.Core.DTOs;
using PerfumeStores.Core.Models;
using PerfumeStores.Core.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;

namespace PerfumeStores.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly Prn221Context _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(Prn221Context context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> Register(RegisterDTO registerDTO)
        {
            var userExist = await _context.Customers.FirstOrDefaultAsync(x => x.Username.Equals(registerDTO.Username));
            if (userExist != null)
            {
                return false;
            }
            var hashSalt = Cryptography.MD5Hash(registerDTO.Password);
            Customer customer = _mapper.Map<Customer>(registerDTO);
            customer.Password = hashSalt;
            customer.IsAdmin = false;
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Customer> Login(LoginDTO loginDTO)
        {
     
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Username.ToLower() == loginDTO.Username.ToLower());
            if (customer != null)
            {
                if (Cryptography.VerifyPassword(loginDTO.Password, customer.Password))
                {
                    LoginDTO.Id = customer.CustomerId;
                    return customer;
                }
            }
            throw new AuthenticationException("Incorrect username or password");
        }
    }
}