using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Accounts
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Hàm lấy ra tài khoản đăng nhập hợp lệ với db
        /// </summary>
        /// <param name="loginModel"> thông khi khi đăng nhập</param>
        /// <returns>Thông tin tài khoản đã đăng nhập thành công</returns>
        /// Created by : TTPhong (21/02/2024)
        Task<UserModel> GetUser(LoginModel loginModel);

        UserModel GetUserByToken(string token);
        int Update(UserModel userModel, Guid userID);
    }
}
