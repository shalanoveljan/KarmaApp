using Karma.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.Entities
{
    public class Author:BaseEntity
    {
        public string FullName { get; set; }

        public string Info { get; set; }

        public int PositionId { get; set; }

        public Position Position { get; set; }

        public List<SocialNetwork> socialNetworks { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
