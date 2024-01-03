using Karma.Core.DTOS;
using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Data.Contexts;
using Karma.Data.Repositories;
using Karma.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarmaApp.Areas.Admin.Controllers
{
        [Area("Admin")]
    public class BrandController : Controller
    {
        readonly IBrandService _brandService;

        public BrandController(IBrandService BrandService)
        {
            _brandService = BrandService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await _brandService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _brandService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _brandService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _brandService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BrandPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(await _brandService.GetAsync(id));
            }

            await _brandService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }




    }
}
