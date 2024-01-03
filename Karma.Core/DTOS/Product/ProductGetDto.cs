using Karma.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.DTOS
{
    public  class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public double Price { get; set; }
        public double DiscountPrice { get; set; }

        public string Description { get; set; } = null!;
        public string Info { get; set; } = null!;
        public CategoryGetDto Category { get; set; } = null!;
        public int CategoryId { get; set; }
        public BrandGetDto Brand { get; set; } = null!;
        public int BrandId { get; set; }
        public List<Specification> Specifications { get; set; } = null!;
        public List<ProductColor> productColors { get; set; } = null!;
        public List<ProductImage> productImages { get; set; } = null!;
    }
}
