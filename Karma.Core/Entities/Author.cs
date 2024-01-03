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
        public string FullName { get; set; } = null!;

        public string Info { get; set; } = null!;

        public int PositionId { get; set; }

        public Position Position { get; set; } = null!;

        public List<SocialNetwork> socialNetworks { get; set; } = null!;
        public List<Blog> Blogs { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Storage { get; set; } = null!;
    }
}
