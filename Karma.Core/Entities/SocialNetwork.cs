using Karma.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.Entities
{
    public class SocialNetwork:BaseEntity
    {
        public string Icon { get; set; }
        public string Url { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }
    }
}
