using Karma.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.Entities
{
    public class Tag:BaseEntity
    {
        public string Name { get; set; }

        public List<TagBlog> tagBlogs { get; set; }
    }
}
