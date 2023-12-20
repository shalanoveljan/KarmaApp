using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.DTOS
{
    public record AuthorPostDto
    {
        public string FullName { get; set; }

        public string Info { get; set; }

        public int PositionId { get; set; }

        public List<string> Icons { get; set; }
        public List<string> Urls { get; set; }
    }
}
