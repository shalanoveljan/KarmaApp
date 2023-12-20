using Karma.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.Entities
{
    public  class Color:BaseEntity
    {
        [StringLength(40)]
        public string ColorName { get; set; } = null!;
    }
}
