using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Resource;
using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMISDemo.Core.Interfaces.Departments;
using MISA.AMISDemo.Core.Interfaces.Base;

namespace MISA.AMISDemo.Core.Services
{
    public class DepartmentService : IBaseService<Department>, IDepartmentService
    {
        IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _departmentRepository = repository;
        }

        public MISAServiceResult InsertService(Department entity)
        {
            var isDuplicate = _departmentRepository.CheckDuplicateDepartmentCode(entity.DepartmentCode);
            if (isDuplicate)
            {
                var serviceResult = new MISAServiceResult
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Data = null,
                };
                serviceResult.Errors.Add(MISAResource.DuplicateCustomerCode);
                return serviceResult;

            }
            var res = _departmentRepository.Insert(entity);
            return new MISAServiceResult
            {
                Success = true,
                Data = res,
            };
        }

        public MISAServiceResult UpdateService(Department entity, Guid id)
        {
            var isDuplicate = _departmentRepository.CheckDuplicateDepartmentCode(entity.DepartmentCode);
            if (isDuplicate)
            {
                var serviceResult = new MISAServiceResult
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Data = null,
                };
                serviceResult.Errors.Add(MISAResource.DuplicateCustomerCode);
                return serviceResult;

            }
            var res = _departmentRepository.Update(entity, id);
            return new MISAServiceResult
            {
                Success = true,
                Data = res,
            };
        }
    }
}
