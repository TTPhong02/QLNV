using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using MISA.AMISDemo.Core.Resource;
using MISA.AMISDemo.Core.Interfaces.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace MISA.AMISDemo.Core.Services.Tokens
{
    public class TokenService : ITokenService
    {
        ITokenRepository _tokenRepository;
        IAccountRepository _accountRepository;
        IConfiguration _configuration;

        public TokenService(ITokenRepository tokenRepository, IConfiguration configuration, IAccountRepository accountRepository)
        {
            _tokenRepository = tokenRepository;
            _configuration = configuration;
            _accountRepository = accountRepository;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public JwtSecurityToken GenerateToken(List<Claim> claims)
        {
            var key = _configuration["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);


            var expires = DateTime.Now.AddMinutes(tokenValidityInMinutes); // Sử dụng UtcNow để tránh vấn đề múi giờ
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            return token;
        }

        public  TokenModel LoginTakeToken(UserModel userModel)
        {
            var authClaims = new List<Claim>();
            {
                new Claim(ClaimTypes.NameIdentifier, userModel.PhoneNumber);
                new Claim(ClaimTypes.Name, userModel.FullName);
                new Claim(ClaimTypes.Email, userModel.Email);
                new Claim(ClaimTypes.Role, userModel.Role);
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            }
            var token = GenerateToken(authClaims);
            var refreshToken = GenerateRefreshToken();
            _ = int.TryParse(_configuration["Jwt:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            userModel.RefreshToken = refreshToken;
            userModel.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
             _accountRepository.Update(userModel,userModel.UserId);
            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = userModel.RefreshToken
            };
        }

        public  TokenModel RefreshToken(TokenModel tokenModel)
        {
            string? accessToken = tokenModel.AccessToken;
            var principal = GetPrincipalFromExpiredToken(accessToken);

            var user =  _accountRepository.GetUserByToken(tokenModel.RefreshToken.ToString());

            var newAccessToken = GenerateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            _accountRepository.Update(user,user.UserId);

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }

        public int UpdateAccount(UserModel userModel)
        {
            return _tokenRepository.UpdateUser(userModel);
        }
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException(MISAResource.InvalidToken);

            return principal;

        }
    }
}
