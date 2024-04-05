using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services
{
    public class PositionService : IBaseService<Positions>, IPositionService
    {
        IPositionRepository _positionRepository;

        public PositionService(IPositionRepository repository)
        {
            _positionRepository = repository;
        }

        public MISAServiceResult InsertService(Positions entity)
        {
            var res = _positionRepository.Insert(entity);
            return new MISAServiceResult
            {
                Success = true,
                Data = res,
            };
        }

        public MISAServiceResult UpdateService(Positions entity, Guid id)
        {
            var res = _positionRepository.Update(entity,id);
            return new MISAServiceResult
            {
                Success = true,
                Data = res,
            };
        }
    }
}
