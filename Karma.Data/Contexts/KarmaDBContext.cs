using Karma.Core.Entities;
using Karma.Core.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Data.Contexts
{
    public class KarmaDBContext:DbContext
	{
		public KarmaDBContext(DbContextOptions<KarmaDBContext> options) : base(options)
		{
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagBlog> tagBlogs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }
        public DbSet<Position> Positions { get; set; }




    }
}
