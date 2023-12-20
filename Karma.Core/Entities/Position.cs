using Karma.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.Entities
{
    public  class Position:BaseEntity
    {
        public string PositionName { get; set; }
        public List<Author> Authors { get; set; }
    }
}
