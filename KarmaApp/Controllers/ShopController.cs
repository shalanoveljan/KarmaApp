using Karma.Core.DTOS;
using Karma.Core.Entities;
using Karma.Data.Contexts;
using Karma.Service.Services.Interfaces;
using KarmaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarmaApp.Controllers
{
	public class ShopController : Controller
	{

        readonly IColorService _colorService;
        readonly ICategoryService _categoryService;
        readonly IBrandService _brandService;
        readonly IProductService _productService;
        readonly IBasketService _basketService;

        public ShopController(ICategoryService categoryService, IBrandService brandService, IColorService colorService, IProductService productService, IBasketService basketService)
        {

            _categoryService = categoryService;
            _brandService = brandService;
            _colorService = colorService;
            _productService = productService;
            _basketService = basketService;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            ShopViewModel shopViewModel = new();
            shopViewModel.categories = await _categoryService.GetAllAsync(page);
            shopViewModel.brands = await _brandService.GetAllAsync(page);
            shopViewModel.colors = await _colorService.GetAllAsync(page);
            shopViewModel.Products = await _productService.GetAllAsync(page);
            return View(shopViewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            return View(await _productService.GetAsync(id));
        }


        public async Task<IActionResult> AddBasket(int id, int count = 1)
        {
            await _basketService.AddBasket(id, count);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Basket()
        {
            return View(await _basketService.GetBasket());
        }

        public async Task<IActionResult> IncreaseCount(int id)
        {
            await _basketService.AddBasket(id, null);
            return RedirectToAction(nameof(Basket));
        }


    }
}
