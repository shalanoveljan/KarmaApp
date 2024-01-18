﻿using System;
namespace Karma.Core.DTOS
{
	public record ColorGetDto
	{
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

