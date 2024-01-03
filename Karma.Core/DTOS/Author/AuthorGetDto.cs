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

        public int Id { get; set; }
        public string FullName { get; set; } = null!;

        public string Info { get; set; } = null!;

        public int PositionId { get; set; }

        public PositionGetDto position { get; set; } = null!;

        public List<string> Icons { get; set; } = null!;
        public List<string> Urls { get; set; } = null!;

        public string Image { get; set; } = null!;


    }
}
