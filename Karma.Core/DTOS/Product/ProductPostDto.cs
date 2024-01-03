using Karma.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.DTOS
{
    public class ProductPostDto
    {
        public string Name { get; set; } = null!;

        public double Price { get; set; }
        public double DiscountPrice { get; set; }

        public string Description { get; set; } = null!;
        public string Info { get; set; } = null!;

        public int BrandId { get; set; }

        public int CategoryId { get; set; }

        public List<int> ColorIds { get; set; } = null!;

        public List<int> Counts { get; set; } = null!;

        public List<string> SpecificationKeys { get; set; } = null!;
        public List<string> SpecificationValues { get; set; } = null!;
        public List<IFormFile> ProductImageFiles { get; set; } = null!;
        public IFormFile MainImageFile { get; set; } = null!;
    }
}
