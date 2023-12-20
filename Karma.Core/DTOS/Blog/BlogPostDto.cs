using Karma.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.DTOS
{
    public class BlogPostDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int AuthorId { get; set; }

        public List<int> TagsIds { get; set; }
    }
}
