using Karma.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.Entities
{
    public class ProductColor:BaseEntity
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }

        public Product Product { get; set; } = null!;

        public Color Color { get; set; } = null!;

        public int StockCount { get; set; }
    }
}
