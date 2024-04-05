using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Accounts;
using MISA.AMISDemo.Core.Services.Accounts;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.UnitTests.Core.Service
{
    [TestFixture]
    public class AccountServiceTests
    {
        public IAccountRepository AccountRepository { get; set; }
        public IAccountService AccountService { get; set;}

        [SetUp]
        public void SetUp()
        {
            AccountRepository = Substitute.For<IAccountRepository>();
            AccountService = new AccountService(AccountRepository);
        }
        /// <summary>
        /// Test cho việc đăng nhập khi đầu vào user không có trong db
        /// </summary>
        /// Created by : TTPHONG(26/02/2024)
        [Test]
        public async Task LoginAsync_UserInvalid_ThrowExeption()
        {
            //Arrange
            LoginModel userLogin = new LoginModel();
            var userModel = new UserModel();
            userModel = null;
            AccountRepository.GetUser(userLogin).Returns(userModel);
            //Act
            var exception = Assert.ThrowsAsync<MISAValidateException>(async() => await AccountService.LoginAsync(userLogin));
            //Assert
            Assert.That(exception.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
        }

        /// <summary>
        /// Test cho việc đăng nhập khi đầu vào user hợp lệ
        /// </summary>
        /// Created by : TTPHONG(26/02/2024)
        [Test]
        public async Task LoginAsync_UserValid_Success()
        {
            //Arrange
            LoginModel userLogin = new LoginModel();
            var userModel = new UserModel();
            userModel.UserId = Guid.NewGuid();
            AccountRepository.GetUser(userLogin).Returns(userModel);
            //Act
            await AccountService.LoginAsync(userLogin);

            //Assert
            await AccountRepository.Received(1).GetUser(userLogin);
        }
    }
}
