using Karma.Core.Repositories;
using Karma.Data.Contexts;
using Karma.Data.Repositories;
using Karma.Service.Services.Implementations;
using Karma.Service.Services.Interfaces;
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
		public static void DataAccessServiceRegister(this IServiceCollection services)
		{
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IBrandService, BrandService>();
			services.AddScoped<IColorService, ColorService>();
			services.AddScoped<IPositionService, PositionService>();
			services.AddScoped<ITagService, TagService>();
			services.AddScoped<IAuthorService, AuthorService>();
			services.AddScoped<IBlogService, BlogService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IProductImageService, ProductImageService>();
			services.AddScoped<IBasketService, BasketService>();
            services.AddHttpContextAccessor();
        }

    }
}
