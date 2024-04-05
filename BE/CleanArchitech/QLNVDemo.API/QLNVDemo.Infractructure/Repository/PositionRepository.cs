using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Interfaces.Position;
using MISA.AMISDemo.Infractructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infractructure.Repository
{
    public class PositionRepository : BaseRepository<Positions>, IPositionRepository 
    {
        public PositionRepository(IMISADbContext dbContext) : base(dbContext)
        {
        }
    }
}
