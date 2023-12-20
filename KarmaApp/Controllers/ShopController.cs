using Karma.Core.DTOS;
using Karma.Core.Entities;
using Karma.Data.Contexts;
using KarmaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarmaApp.Controllers
{
	public class ShopController : Controller
	{

		readonly KarmaDBContext _context;

		public ShopController(KarmaDBContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
            ShopViewModel shopVM = new ShopViewModel
            {
                categories = _context.Categories.Where(x => !x.iSDeleted).AsNoTracking().Select(x => new CategoryGetDto { CategoryName = x.CategoryName }).AsEnumerable(),
                brands = _context.Brands.Where(x => !x.iSDeleted).AsNoTracking().Select(x=>new BrandGetDto { BrandName=x.Name}).AsEnumerable(),
                colors = _context.Colors.Where(x => !x.iSDeleted).AsNoTracking().Select(x=>new ColorGetDto { ColorName=x.ColorName}).AsEnumerable()

            };

         
            return View(shopVM);
		}
        public IActionResult Create()
        {
            return View();
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist=_context.Categories.Any(c=>c.CategoryName== category.CategoryName);

          //  Category? exist=_context.Categories.FirstOrDefault(x => x.CategoryName.ToLower()==category.CategoryName.ToLower());

            if (isExist)
            {
                ModelState.AddModelError("CategoryName", "Category already exist");
                return View();
            }

			_context.Categories.Add(category);	
			_context.SaveChanges();
            return RedirectToAction("Index","Shop");
        }

        public IActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBrand(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Brand? exist = _context.Brands.FirstOrDefault(x => x.Name.ToLower() == brand.Name.ToLower());

            if (exist != null)
            {
                ModelState.AddModelError("", "Brand already exist");
            }

            _context.Brands.Add(brand);
            _context.SaveChanges();
            return RedirectToAction("Index", "Shop");
        }

        public IActionResult CreateColor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateColor(Color color)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Color? exist = _context.Colors.FirstOrDefault(x => x.ColorName.ToLower() == color.ColorName.ToLower());

            if (exist != null)
            {
                ModelState.AddModelError("", "Color already exist");
            }

            _context.Colors.Add(color);
            _context.SaveChanges();
            return RedirectToAction("Index", "Shop");
        }

    }
}
