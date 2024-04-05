using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Resource;
using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMISDemo.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using OfficeOpenXml;
using MISA.AMISDemo.Core.Interfaces.Employees;
using MISA.AMISDemo.Core.Interfaces;

namespace MISA.AMISDemo.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public EmployeeService(IEmployeeRepository repository, IUnitOfWork unitOfwork, IMapper mapper): base(repository) 
        {
            _employeeRepository = repository;
            _unitOfWork = unitOfwork;
            this.mapper = mapper;
        }



        public override MISAServiceResult InsertService(Employee entity)
        {
            var isDuplicate = _employeeRepository.CheckDuplicateEmployeeCode(entity.EmployeeCode);
            if (isDuplicate)
            {
                throw new MISAValidateException(MISAResource.DuplicateCustomerCode);

            }
            var res = _employeeRepository.Insert(entity);
            return new MISAServiceResult
            {
                Success = true,
                Data = res,
            };
        }

        public override MISAServiceResult UpdateService(Employee entity, Guid id)
        {
            var isDuplicate = _employeeRepository.CheckDuplicateEmployeeCode(entity.EmployeeCode);
            if (isDuplicate)
            {
                throw new MISAValidateException(MISAResource.DuplicateCustomerCode);
            }
            var res = _employeeRepository.Update(entity, id);
            return new MISAServiceResult
            {
                Success = true,
                Data = res,
            };
        }

        public IEnumerable<Employee> ImportEmployee(IFormFile fileImport)
        {
            CheckFileImport(fileImport);
            var employees = new List<EmployeeImport>();
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
                            var employeeImport = new EmployeeImport()
                            {
                                EmployeeId = Guid.NewGuid(),
                                EmployeeCode = worksheet?.Cells[row, 2]?.Value?.ToString()?.Trim(),
                                FullName = worksheet?.Cells[row, 3]?.Value?.ToString()?.Trim(),
                                PhoneNumber = worksheet?.Cells[row, 13]?.Value?.ToString()?.Trim(),
                                //DateOfBirth = ProcessDate(dob),
                                DateOfBirth = ProcessDate(dob),

                                Email = worksheet?.Cells[row, 14]?.Value?.ToString()?.Trim(),
                                Address = worksheet?.Cells[row, 18]?.Value?.ToString()?.Trim(),
                                IdentityNumber = worksheet?.Cells[row, 6]?.Value?.ToString()?.Trim(),
                                IdentityPlace = worksheet?.Cells[row, 7]?.Value?.ToString()?.Trim(),
                            };
                            //Validate dữ liệu
                            // 1. Kiểm trả không có mã nhân viên
                            var isDuplicate = _unitOfWork.Employees.CheckDuplicateEmployeeCode(employeeImport.EmployeeCode);
                            if (employeeImport.EmployeeCode == null || employeeImport.EmployeeCode == "")
                            {
                                employeeImport.ImportErrors.Add(MISAResource.EmployeeCodeNotEmpty);
                            }
                            // 2 . Kiểm tra trùng mã
                            if (isDuplicate)
                            {
                                employeeImport.ImportErrors.Add(MISAResource.DuplicateEmployeeCode);
                            }
                            // 3 . Kiểm tra FullName trống 
                            if (employeeImport.FullName == null || employeeImport.FullName == "")
                            {
                                employeeImport.ImportErrors.Add(MISAResource.FullNameNotEmpty);
                            }
                            if (employeeImport.ImportErrors?.Count == 0)
                            {
                                var employee = mapper.Map<Employee>(employeeImport);
                                //Thực hiện thêm mới
                                var res = _unitOfWork.Employees.Insert(employee);
                                if (res > 0)
                                {
                                    employeeImport.IsImported = true;
                                }
                            }
                            employees.Add(employeeImport);
                        }
                    }
                    _unitOfWork.Commit();
                }
            }
            return employees;
        }
        protected override void ValidateObject(Employee entity)
        {
            // Thực hiện kiểm tra mã khách hàng
            var isDuplicate = _employeeRepository.CheckDuplicateEmployeeCode(entity.EmployeeCode);
            if (isDuplicate)
            {
                throw new MISAValidateException(MISAResource.DuplicateCustomerCode);
            }

        }
        private DateTime? ProcessDate(object? dateValue)
        {
            if (dateValue != null)
            {
                DateTime date;
                if (DateTime.TryParse(dateValue.ToString(), out date))
                {
                    return date;
                }
            }
            return null;
        }
        private void CheckFileImport(IFormFile importFile)
        {
            if (importFile == null || importFile.Length <= 0)
            {
                throw new MISAValidateException(MISAResource.InvalidFileImport);
            }
            if (!Path.GetExtension(importFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new MISAValidateException(MISAResource.InvalidFileImportFormat);
            }
        }
    }
}
