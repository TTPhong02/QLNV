using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Tokens
{
    public interface ITokenService
    {
        string GenerateRefreshToken();
        TokenModel LoginTakeToken(UserModel userModel);

        TokenModel RefreshToken(TokenModel tokenModel);
        int UpdateAccount(UserModel userModel);

    }
}
