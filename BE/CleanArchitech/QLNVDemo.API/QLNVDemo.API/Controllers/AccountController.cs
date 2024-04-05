using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Accounts;
using MISA.AMISDemo.Core.Interfaces.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MISA.AMISDemo.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountService _accountService;
        ITokenRepository _tokenRepository;
        ITokenService _tokenService;
        IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration, ITokenRepository tokenRepository, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _tokenRepository = tokenRepository;
            _accountService = accountService;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel modelLogin)
        {
            var user = await _accountService.LoginAsync(modelLogin);
            var token =  _tokenService.LoginTakeToken(user);
            return Ok(new {User = user, Token = token});    
        }

        [HttpPost]
        [Route("refresh-token")]
        public IActionResult RefreshToken(TokenModel tokenModel)
        {

                var refreshToken = _tokenService.RefreshToken(tokenModel);
                return Ok(refreshToken);
            
        }



    }
}
