using Karma.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KarmaApp.Controllers
{
	public class HomeController : Controller
	{
		readonly KarmaDBContext _context;

		public HomeController(KarmaDBContext context)
		{
			_context = context;
		}

        public IActionResult Index()
        {

            return View();
        }


    }
}