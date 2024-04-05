using AutoMapper;
using MISA.AMISDemo.Core.Resource;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Interfaces.Employees;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Services;
using MISA.AMISDemo.Core.Exceptions;

namespace MISA.AMISDemo.UnitTests.Service
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IEmployeeService EmployeeService { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        public IMapper Mapper { get; set; }

        /// <summary>
        /// Hàm thực hiện thiết lập cho các class test
        /// </summary>
        /// Created by : TTPhong(26/2/2023)
        [SetUp]
        public void SetUp()
        {
            EmployeeRepository = Substitute.For<IEmployeeRepository>();
            UnitOfWork = Substitute.For<IUnitOfWork>();
            EmployeeService = new EmployeeService(EmployeeRepository, UnitOfWork, Mapper);
            Mapper = Substitute.For<IMapper>();
        }

        /// <summary>
        /// Test cho việc thêm mới khi đầu vào mã nhân viên bị trùng  
        /// </summary>
        /// Created By: TTPhong(26/2/2023)
        //[Test]
        //public void InsertService_EmployeeCodeDuplicate_ThrowException()
        //{
        //    //Arrange
        //   var employee = new Employee();
        //    EmployeeRepository.CheckDuplicateEmployeeCode(employee.EmployeeCode).Returns(true);
        //    //Act
        //    var exception = Assert.Throws<MISAServiceResult>(EmployeeService.InsertService(employee));
        //    //Assert
        //    Assert.That(exception.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
        //}
        /// <summary>
        /// Test cho việc thực hiện thêm mới khi mã nhân viên hợp lệ
        /// </summary>
        [Test]
        public void InsertService_EmployeeCodeValid_Success()
        {
            //Arrange
            var employee = new Employee();
            EmployeeRepository.CheckDuplicateEmployeeCode(employee.EmployeeCode).Returns(false);
            EmployeeRepository.Insert(employee).Returns(1);
            //Act
            var expectResult = EmployeeService.InsertService(employee);
            //Assert
            Assert.That(expectResult.Data, Is.EqualTo(1));
            EmployeeRepository.Received(1).Insert(employee);

        }
        /// <summary>
        /// Test cho việc cập nhật khi đầu vào mã nhân viên bị trùng
        /// </summary>
        /// Created by : TTPHONG(26/02/2024)
        //[Test]
        //public void UpdateService_EmployeeCodeDuplicate_ThrowException()
        //{
        //    //Arrange
        //    var employee = new Employee();
        //    EmployeeRepository.CheckDuplicateEmployeeCode(employee.EmployeeCode).Returns(true);
        //    //Act
        //    var exception = Assert.Throws<MISAServiceResult>(()=>EmployeeService.UpdateService(employee,employee.EmployeeId));
        //    //Assert
        //    Assert.That(exception.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
        //}

        /// <summary>
        /// Test cho việc cập nhật khi đầu vào mã nhân viên bị trùng
        /// </summary>
        /// Created by : TTPHONG(26/02/2024)
        [Test]
        public void UpdateService_EmployeeCodeValid_Success()
        {
            //Arrange
            var employee = new Employee();
            EmployeeRepository.CheckDuplicateEmployeeCode(employee.EmployeeCode).Returns(false);
            EmployeeRepository.Update(employee,employee.EmployeeId).Returns(1);
            //Act
            var expectResult = EmployeeService.UpdateService(employee,employee.EmployeeId);
            //Assert
            Assert.That(expectResult.Data, Is.EqualTo(1));
            EmployeeRepository.Received(1).Update(employee,employee.EmployeeId);
        }
    }
}
