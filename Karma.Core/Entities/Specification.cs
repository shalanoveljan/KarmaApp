using Karma.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.Entities
{
    public  class Specification:BaseEntity
    {
        public string Key { get; set; } = null!;

        public string Value { get; set; } = null!;

        public int  ProductId { get; set; }

        public Product Product { get; set; } = null!;
    }
}
