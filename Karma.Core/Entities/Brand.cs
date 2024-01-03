using Karma.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.Entities
{
    public class Brand:BaseEntity
    {
        [StringLength(40)]
        public string Name { get; set; } = null!;

        public List<Product> Products { get; set; } = null!;


    }
}
