using Karma.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.DTOS
{
    public class BlogPostDto
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public IFormFile? ImageFile { get; set; }

        public AuthorGetDto? authorGetDto { get; set; }


        public int AuthorId { get; set; }

        public List<int> TagsIds { get; set; } = null!;
    }
}
