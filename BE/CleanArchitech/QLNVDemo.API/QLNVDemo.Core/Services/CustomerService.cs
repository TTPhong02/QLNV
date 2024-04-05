using MISA.AMISDemo.Core.Resource;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using MISA.AMISDemo.Core.Exceptions;
using AutoMapper;
using MISA.AMISDemo.Core.Interfaces.Customers;
using MISA.AMISDemo.Core.Interfaces;

namespace MISA.AMISDemo.Core.Services
{
    public class CustomerService :BaseService<Customer>, ICustomerService
    {
        ICustomerRepository _customerRepository;
        IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public CustomerService(ICustomerRepository repository, IUnitOfWork unitOfwork, IMapper mapper) : base(repository)
        {
            _customerRepository = repository;
            _unitOfWork = unitOfwork;
            this.mapper = mapper;
        }

        public IEnumerable<Customer> ImportCustomer(IFormFile fileImport)
        {
            CheckFileImport(fileImport);
            var customers = new List<CustomerImport>();
            using (var stream = new MemoryStream())
            {
                fileImport.CopyTo(stream);
                using (var excelPackage = new ExcelPackage(stream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets[0];
                    _unitOfWork.BeginTransaction();
                    if (worksheet != null)
                    {
                        var rowCount = worksheet.Dimension.Rows;
                        for (var row = 3; row <= rowCount; row++)
                        {
                            var dob = worksheet.Cells[row, 6]?.Value?.ToString()?.Trim();
                            var customerImport = new CustomerImport()
                            {
                                CustomerId = Guid.NewGuid(),
                                CustomerCode = worksheet?.Cells[row, 1]?.Value?.ToString()?.Trim(),
                                FullName = worksheet?.Cells[row, 2]?.Value?.ToString()?.Trim(),
                                PhoneNumber = worksheet?.Cells[row, 5]?.Value?.ToString()?.Trim(),
                                DateOfBirth = ProcessDate(dob),
                                Email = worksheet?.Cells[row, 9]?.Value?.ToString()?.Trim(),
                                Address = worksheet?.Cells[row, 10]?.Value?.ToString()?.Trim(),
                            };
                            //Validate dữ liệu
                            // 1. Kiểm trả không có mã khách hàng
                            var isDuplicate =  _unitOfWork.Customers.CheckDuplicateCustomerCode(customerImport.CustomerCode);
                            if (customerImport.CustomerCode == null || customerImport.CustomerCode == "")
                            {
                                customerImport.ImportErrors.Add(MISAResource.CustomerCodeNotEmpty);
                            }
                            // 2 . Kiểm tra trùng mã
                            if (isDuplicate)
                            {
                                customerImport.ImportErrors.Add(MISAResource.DuplicateCustomerCode);
                            }
                            // 3 . Kiểm tra Email trống 
                            if(customerImport.Email == null || customerImport.Email == "")
                            {
                                customerImport.ImportErrors.Add(MISAResource.EmailNotEmpty);
                            }
                            if (customerImport.ImportErrors?.Count == 0)
                            {
                                var customer = mapper.Map<Customer>(customerImport);
                                //Thực hiện thêm mới
                                var res =  _unitOfWork.Customers.Insert(customer);
                                if (res > 0)
                                {
                                    customerImport.IsImported = true;
                                }
                            }
                            customers.Add(customerImport);
                        }
                    }
                    _unitOfWork.Commit();
                }
            }
            return customers;
        }
        public override MISAServiceResult InsertService(Customer entity)
        {
            var isDuplicate = _customerRepository.CheckDuplicateCustomerCode(entity.CustomerCode);
            if (isDuplicate)
            {
                throw new MISAValidateException(MISAResource.DuplicateCustomerCode);

            }
            var res = _customerRepository.Insert(entity);
            return new MISAServiceResult {
                Success = true,
                Data = res,
            };
        }

        public override MISAServiceResult UpdateService(Customer entity, Guid id)
        {
            var isDuplicate = _customerRepository.CheckDuplicateCustomerCode(entity.CustomerCode);
            if (isDuplicate)
            {
                throw new MISAValidateException(MISAResource.DuplicateCustomerCode);
            }
            var res = _customerRepository.Update(entity, id);
            return new MISAServiceResult
            {
                Success = true,
                Data = res,
            };
        }

        private DateTime? ProcessDate(object? dateValue)
        {
            if(dateValue != null)
            {
                DateTime date;
                if(DateTime.TryParse(dateValue.ToString(), out date))
                {
                    return date;
                }
            }
            return null;
        }
        private void CheckFileImport(IFormFile importFile)
        {
            if(importFile == null || importFile.Length <= 0)
            {
                throw new MISAValidateException(MISAResource.InvalidFileImport);
            }
            if (!Path.GetExtension(importFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)){
                throw new MISAValidateException(MISAResource.InvalidFileImportFormat);
            }
        }
        protected override void ValidateObject(Customer entity)
        {
            // Thực hiện kiểm tra mã khách hàng
            var isDuplicate = _customerRepository.CheckDuplicateCustomerCode(entity.CustomerCode);
            if (isDuplicate)
            {
                throw new MISAValidateException(MISAResource.DuplicateCustomerCode);
            }

        }
    }
}
