using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.DTOS
{
    public record CategoryGetDto
    {
        [StringLength(40)]
        public string CategoryName { get; set; } = null!;
    }
}
