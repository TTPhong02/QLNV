using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Accounts
{
    public interface IAccountService
    {
        /// <summary>
        /// Hàm thực hiện đăng nhập (kiểm tra xem thông tin đăng nhập có hợp lệ hay không )
        /// </summary>
        /// <param name="loginModel"> thông tin đăng nhập</param>
        /// <returns> Thông tin tài khoản khi đăng nhập thành công </returns>
        /// Created by : TTPhong (21/02/2024)
        Task<UserModel> LoginAsync(LoginModel loginModel);

    }
}
