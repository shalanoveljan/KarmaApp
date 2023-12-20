using Karma.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.Entities
{
    public class Blog:BaseEntity
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public int ViewCount { get; set; }

        public string Image { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }
        public List<TagBlog> tagBlogs { get; set; }

    }
}
