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
    public class BlogController : Controller
    {
        readonly IBlogService _blogService;
        readonly IAuthorService _authorService;
        readonly ITagService _tagService;


        public BlogController(IBlogService BlogService, IAuthorService AuthorService, ITagService TagService)
        {
            _blogService = BlogService;
            _authorService = AuthorService;
            _tagService = TagService;
        }

        public async Task<IActionResult> Index(int page=1)
        {

            return View(await _blogService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _authorService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _authorService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                return View();
            }
            var response = await _blogService.CreateAsync(dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Authors = await _authorService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();

                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {

            ViewBag.Authors = await _authorService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();

            return View(await _blogService.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _authorService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();

                return View(await _blogService.GetAsync(id));
            }
            var response = await _blogService.UpdateAsync(id, dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Authors = await _authorService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();

                ModelState.AddModelError("", response.Message);
                return View(await _blogService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _blogService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }



    }
}
