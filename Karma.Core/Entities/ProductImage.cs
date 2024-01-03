using Karma.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.Entities
{
    public class ProductImage:BaseEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;
        public bool IsMain { get; set; }

        public string Image { get; set; } = null!;
    }
}
