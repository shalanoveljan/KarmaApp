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

        public string Title { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public AuthorGetDto authorGetDto { get; set; }

        public IEnumerable<TagGetDto> tagGetDtos { get; set; }

        public int ViewCount { get; set; }

        public DateTime Date { get; set; }




    }
}
