using Karma.Core.Repositories;
using Karma.Data.Contexts;
using Karma.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Data.ServiceRegistrations
{
	public static class DataAccessServiceRegisterExtension
	{
		public static void DataAccessServiceRegister(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddDbContext<KarmaDBContext>(option =>
			{
				option.UseSqlServer(configuration.GetConnectionString("Default"));
			});

			services.AddScoped<IBrandRepository, BrandRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IColorRepository, ColorRepository>();
			services.AddScoped<IPositionRepository, PositionRepository>();
			services.AddScoped<ITagRepository, TagRepository>();
			services.AddScoped<IAuthorRepository, AuthorRepository>();
			services.AddScoped<IBlogRepository, BlogRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IProductImageRepository, ProductImageRepository>();
        }

	}
}
