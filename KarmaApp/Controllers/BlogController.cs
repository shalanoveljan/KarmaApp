using Karma.Core.DTOS;
using Karma.Core.Entities;
using Karma.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace KarmaApp.Controllers
{
    public class BlogController : Controller
    {

        readonly KarmaDBContext _context;

        public BlogController(KarmaDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<BlogGetDto> blogs = await _context.Blogs.Where(x => !x.iSDeleted)
                .Include(x => x.tagBlogs)
                .ThenInclude(x => x.Tag)
                .Include(x => x.Author)
                .AsNoTrackingWithIdentityResolution()
                .Select(blog => new BlogGetDto
                {
                    Description = blog.Description,
                    Image = blog.Image,
                    Title = blog.Title,
                    ViewCount = blog.ViewCount,
                    authorGetDto = new AuthorGetDto { FullName = blog.Author.FullName },
                    Date=blog.CreateAt,
                    Id= blog.Id,

                    tagGetDtos = blog.tagBlogs.Select(tagblog => new TagGetDto { TagName = tagblog.Tag.Name })
                }).ToListAsync();
            return View(blogs);
        }


        public  async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _context.Authors.Where(x => !x.iSDeleted).AsNoTrackingWithIdentityResolution().ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(x => !x.iSDeleted).AsNoTracking().ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogPostDto blogDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _context.Authors.Where(x => !x.iSDeleted).AsNoTracking().ToListAsync();
                ViewBag.Tags = await _context.Tags.Where(x => !x.iSDeleted).AsNoTracking().ToListAsync();
                return View();
            }

            bool exist = await _context.Authors.AnyAsync(x=>x.Id==blogDto.AuthorId);

            if(!exist)
            {
                ViewBag.Authors = await _context.Authors.Where(x => !x.iSDeleted).AsNoTracking().ToListAsync();
                ViewBag.Tags = await _context.Tags.Where(x => !x.iSDeleted).AsNoTracking().ToListAsync();
                ModelState.AddModelError("AuthorId", "Agilli ol");
                return View();  
            }
            Blog blog = new Blog
            {
                CreateAt = DateTime.Now,
                Image = blogDto.Image,
                Title = blogDto.Title,
                Description = blogDto.Description,
                AuthorId = blogDto.AuthorId,


            };
            await _context.Blogs.AddAsync(blog);

            foreach (var item in blogDto.TagsIds)
            {
                TagBlog tagBlog = new TagBlog
                {
                    CreateAt = DateTime.Now,
                    Blog = blog,
                    TagId = item
                };

                await _context.tagBlogs.AddAsync(tagBlog);
            }


            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var query= _context.Blogs.Where(x=>!x.iSDeleted && x.Id==id)
                .Include(x=>x.tagBlogs)
                .ThenInclude(x=>x.Tag)
                .Include(x=>x.Author)
                .AsNoTrackingWithIdentityResolution();

            if (query.Count() == 0)
            {
                return NotFound();
            }

            await IncreaseCount(id);
            BlogGetDto? blogGetDto = await query.Select(blog => new BlogGetDto
            {
                Date = blog.CreateAt,
                Description = blog.Description,
                Image = blog.Image,
                Title = blog.Title,
                ViewCount = blog.ViewCount,

                authorGetDto = new AuthorGetDto { FullName = blog.Author.FullName, Info = blog.Author.Info },
                tagGetDtos = blog.tagBlogs.Select(X => new TagGetDto { TagName = X.Tag.Name })
            }).FirstOrDefaultAsync();

            

            return View(blogGetDto);
        }

        private async Task IncreaseCount(int id)
        {
            Blog? blog = await _context.Blogs.FindAsync(id);

            if(blog != null) { 
            blog.ViewCount++;
            }

            await _context.SaveChangesAsync();
        }




        
    }
}
