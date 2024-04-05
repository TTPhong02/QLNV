using Dapper;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Tokens;
using MISA.AMISDemo.Infractructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infractructure.Repository
{
    

    public class TokenRepository : ITokenRepository
    {
        IMISADbContext _dbContext;
        public TokenRepository(IMISADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<UserModel> GetUserByPhoneNumber(string phoneNumber)
        {
            var sql = "Proc_User_GetUserByPhoneNumber";
            var user = _dbContext.Connection.QueryFirstOrDefault(sql, phoneNumber, commandType: System.Data.CommandType.StoredProcedure);
            return user;
        }

        public  Task<UserModel> GetUserByRefreshToken(string refreshToken)
        {
            var sql = "Proc_User_GetUserByRefreshToken";
            var user =  _dbContext.Connection.QueryFirstOrDefault(sql,refreshToken, commandType: System.Data.CommandType.StoredProcedure);
            return user;
        }

        public int UpdateUser(UserModel user)
        {
            var sql = "Proc_User_Update";
            return _dbContext.Connection.Execute(sql, user, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
