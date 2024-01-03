using Karma.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.DTOS
{
    public class BlogGetDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Image { get; set; } = null!;

        public string Description { get; set; } = null!;

        public AuthorGetDto? authorGetDto { get; set; }

        public IEnumerable<TagGetDto> tagGetDtos { get; set; } = null!;

        public int ViewCount { get; set; }

        public DateTime Date { get; set; }

        public PositionGetDto position { get; set; } = null!;

        public List<string> Icons { get; set; } = null!;
        public List<string> Urls { get; set; } = null!;




    }
}
