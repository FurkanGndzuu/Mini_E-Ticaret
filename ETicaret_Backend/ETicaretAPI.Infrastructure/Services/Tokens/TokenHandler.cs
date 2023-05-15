using ETicareAPI.Domain.Entities.IdentityEntities;
using ETicaretAPI.Application.Abstractions.Tokens;
using ETicaretAPI.Application.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Tokens
{
    public class TokenHandler : ITokenHandler
    {

        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(int minutes ,AppUser user)
        {

            Token token = new Token();


            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:securityKey"]));

            SigningCredentials creadentails = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddSeconds(minutes);

            JwtSecurityToken securityToken = new(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires : token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials : creadentails,
                claims :new  List<Claim> {new(ClaimTypes.Name , user.UserName) }
                
                
                );

            JwtSecurityTokenHandler tokenHandler = new();

            token.AccesToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();

            return token;


        }

        public string CreateRefreshToken()
        {
            byte[] refreshTokenByte = new byte[32];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(refreshTokenByte);
            return Convert.ToBase64String(refreshTokenByte);

        }
    }
}
