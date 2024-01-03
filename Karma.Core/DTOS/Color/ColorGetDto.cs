using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.DTOS
{
    public  record ColorGetDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ProductCount { get; set; }

        public string ColorName { get; set; } = null!;
    }
}
