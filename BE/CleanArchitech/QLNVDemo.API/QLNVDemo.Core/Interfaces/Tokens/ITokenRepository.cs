using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Tokens
{
    public interface ITokenRepository
    {
        Task<UserModel> GetUserByRefreshToken(string refreshToken);
        Task<UserModel> GetUserByPhoneNumber(string phoneNumber);
        int UpdateUser(UserModel user);
    }
}
