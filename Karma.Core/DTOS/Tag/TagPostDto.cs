using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.DTOS
{
    public record TagPostDto
    {
        public string TagName { get; set; } = null!;
    }
}
