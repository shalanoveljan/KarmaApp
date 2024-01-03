using Karma.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; } = null!;

        public double Price { get; set; }
        public double DiscountPrice { get; set; }

        public string Description { get; set; } = null!;
        public string Info { get; set; } = null!;

        public Category Category { get; set; } = null!;

        public int BrandId { get; set; }

        public Brand Brand { get; set; } = null!;

        public int CategoryId { get; set; }

        public List<Specification> Specifications { get; set; } = null!;
        public List<ProductColor> productColors { get; set; } = null!;
        public List<ProductImage> productImages { get; set; } = null!;
    }
}
