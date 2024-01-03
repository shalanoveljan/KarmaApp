using Karma.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.Entities
{
    public class TagBlog:BaseEntity
    {

        public Blog Blog { get; set; } = null!;
        public int BlogId { get; set; }
        public Tag Tag { get; set; } = null!;

        public int TagId { get; set; }
    }
}
