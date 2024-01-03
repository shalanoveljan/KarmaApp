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
        public int Id { get; set; } 
        public string CategoryName { get; set; } = null!;
        public int ProductCount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
