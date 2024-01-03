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
    public class TagController : Controller
    {
        readonly ITagService _tagService;

        public TagController(ITagService TagService)
        {
            _tagService = TagService;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            return View(await _tagService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _tagService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _tagService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _tagService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, TagPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(await _tagService.GetAsync(id));
            }

            await _tagService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }




    }
}
