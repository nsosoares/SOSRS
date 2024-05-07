﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SOSRS.Api.Configuration;
using SOSRS.Api.Data;
using SOSRS.Api.Entities;
using SOSRS.Api.ViewModels.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SOSRS.Api.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly JWTConfiguration _configuration;
        private readonly AppDbContext _db;

        public AuthService(JWTConfiguration configuration, AppDbContext db)
        {
            _configuration =
                configuration
                ?? throw new ArgumentNullException(nameof(configuration));
            _db = db;
        }

        public string GenerateJWTToken(UserAuthJWTClaims userClaims)
        {
            var claims = new[]
            {
                new Claim("id", userClaims.UserId.ToString()),
                new Claim("nome", userClaims.Nome),
                new Claim("email", userClaims.User),
                new Claim("cpf", userClaims.CPF),
                new Claim("abrigos", string.Join('|', userClaims.UserAbrigosId))
            };

            var token = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
                signingCredentials:
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
                        SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> RegisterUserAsync(string email, string password, string cpf, string telefone)
        {
            _db.Usuario.Add(new Usuario(Guid.NewGuid(), email, password, cpf, telefone));
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<UserLoginResponse> SignInUserAsync(string user, string password)
        {
            var userExistent = await _db.Usuario.FirstOrDefaultAsync(u => u.User == user && u.Password == password);
            if (userExistent == null)
            {
                return new UserLoginResponse();
            }

            var abrigosIds = await _db.Abrigos
                .Where(a => a.UsuarioId == userExistent.Id)
                .Select(a => a.Id)
                .ToListAsync();
            var userJwt = new UserAuthJWTClaims
            {
                UserId = userExistent.Id,
                Nome = userExistent.User,
                CPF = userExistent.Cpf,
                Telefone = userExistent.Telefone,
            };


            var token = GenerateJWTToken(userJwt);
            var response = new UserLoginResponse()
            {
                Token = token,
                UserInfo = new UserAuthJWTClaims
                {
                    UserId = Guid.NewGuid(),
                    Nome = userJwt.Nome,
                    User = userJwt.User,
                    CPF = userJwt.CPF,
                    UserAbrigosId = abrigosIds
                }
            };

            return response;
          

    }
}
}
