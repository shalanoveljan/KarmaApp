using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Data.Repositories
{
    public class PositionRepository:Repository<Position>,IPositionRepository
    {
       public PositionRepository(KarmaDBContext context):base(context)
        { 

        }
    }
}
