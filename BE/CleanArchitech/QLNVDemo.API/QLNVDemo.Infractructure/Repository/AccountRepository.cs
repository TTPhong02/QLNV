using Dapper;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Accounts;
using MISA.AMISDemo.Infractructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infractructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        IMISADbContext _dbContext;

        public AccountRepository(IMISADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserModel> GetUser(LoginModel loginModel) 
        {
            var sql = "Proc_Account_GetAccountValid";
            var user = await  _dbContext.Connection.QueryFirstOrDefaultAsync<UserModel>(sql,loginModel);
            return user;
        }

        public  UserModel GetUserByToken(string token)
        {
            var sql = "Proc_User_GetUserByRefreshToken";
            var parameter = new DynamicParameters(token);
            parameter.Add("@RefreshToken", token);
            var user =  _dbContext.Connection.QueryFirstOrDefault<UserModel>(sql,parameter);
            return user;
        }

        public string MD5Hash(string password)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString().ToLower();
        }

        public int Update(UserModel userModel, Guid userID)
        {
            var res =   _dbContext.Update<UserModel>(userModel, userID);
            return res;
        }
    }
}
