using Karma.Core.DTOS;
using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Data.Contexts;
using Karma.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace KarmaApp.Controllers
{
    public class BlogController : Controller
    {

        readonly KarmaDBContext _context;
        readonly IBlogService _blogService;

        public BlogController(KarmaDBContext context, IBlogService BlogService)
        {
            _context = context;
            _blogService = BlogService;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            var Blogs=await _blogService.GetAllAsync(page);
            ViewBag.Tags = _context.Tags.Where(x=>!x.iSDeleted)
                .Include(x=>x.tagBlogs)
                .ThenInclude(x=>x.Blog)
                .Select(tag=> new {Name=tag.Name,Count=tag.tagBlogs.Where(x=>!x.Blog.iSDeleted).Count()}).AsNoTrackingWithIdentityResolution()
                .ToListAsync();
            return View(Blogs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var blogGetDto = await _blogService.GetAsync(id);


            await IncreaseCount(id);

            ViewBag.Tags = _context.Tags.Where(x => !x.iSDeleted)
          .Include(x => x.tagBlogs)
          .ThenInclude(x => x.Blog)
          .Select(tag => new { Name = tag.Name, Count = tag.tagBlogs.Where(x => !x.Blog.iSDeleted).Count() }).AsNoTrackingWithIdentityResolution();

            return View(blogGetDto);
        }

        private async Task IncreaseCount(int id)
        {
            Blog? blog = await _context.Blogs.FindAsync(id);

            if (blog != null)
            {
                blog.ViewCount++;
            }

            await _context.SaveChangesAsync();
        }



    }



        
    
}
