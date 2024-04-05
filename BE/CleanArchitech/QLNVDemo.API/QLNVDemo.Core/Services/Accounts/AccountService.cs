using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Accounts;
using MISA.AMISDemo.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services.Accounts
{
    public class AccountService : IAccountService
    {
        IAccountRepository _accountRepository;
        Dictionary<string, string[]>? errors = new Dictionary<string, string[]>();
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<UserModel> LoginAsync(LoginModel loginModel)
        {
            var user  = await _accountRepository.GetUser(loginModel);
            if (user != null)
            {
                return user;
            }
            else
            {
                errors.Add("Account", new string[] { MISAResource.AccountInValid });
                throw new MISAValidateException(System.Net.HttpStatusCode.Unauthorized, MISAResource.AccountInValid, errors);
            }
        }
    }
}
