using Karma.Core.DTOS;
using Karma.Core.Entities;
using Karma.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarmaApp.Controllers
{
    public class TagController : Controller
    {

        readonly KarmaDBContext _context;

        public TagController(KarmaDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<TagGetDto> Tags = _context.Tags.AsNoTracking().Where(p => !p.iSDeleted).Select(p => new TagGetDto { TagName = p.Name }).ToList();
            return View(Tags);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagPostDto TagDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist = _context.Tags.Any(t => t.Name == TagDto.TagName);


            if (isExist)
            {
                ModelState.AddModelError("", $"{TagDto}----Tag already exist");
                return View();
            }

            Tag NewTag = new Tag();

            NewTag.Name = TagDto.TagName;

            _context.Tags.Add(NewTag);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }


    }
}
