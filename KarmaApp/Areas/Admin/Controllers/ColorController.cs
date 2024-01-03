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
    public class ColorController : Controller
    {
        readonly IColorService _colorService;

        public ColorController(IColorService ColorService)
        {
            _colorService = ColorService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await _colorService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ColorPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _colorService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _colorService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _colorService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ColorPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(await _colorService.GetAsync(id));
            }

            await _colorService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }



    }
}
