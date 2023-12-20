using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.DTOS
{
    public record PositionPostDto
    {
        public string PositionName { get; set; } = null!;
    }
}
