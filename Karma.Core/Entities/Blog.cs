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

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int AuthorId { get; set; }
        public string Image { get; set; } = null!;

        public int ViewCount { get; set; }



        public Author Author { get; set; } = null!;
        public List<TagBlog> tagBlogs { get; set; } = null!;

    }
}
