using Karma.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Core.DTOS
{
    public record TagGetDto
    {
        public string TagName { get; set; } = null!;


    }
}
