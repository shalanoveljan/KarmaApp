using Karma.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.DTOS
{
    public record AuthorGetDto
    {
        public string FullName { get; set; }

        public string Info { get; set; }

        public int PositionId { get; set; }


    }
}
