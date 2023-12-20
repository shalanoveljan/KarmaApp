using Karma.Data.Contexts;
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
		}

	}
}
