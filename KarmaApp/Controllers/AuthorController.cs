using Karma.Core.DTOS;
using Karma.Core.Entities;
using Karma.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarmaApp.Controllers
{
    public class AuthorController : Controller
    {

        readonly KarmaDBContext _context;

        public AuthorController(KarmaDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Authors.AsNoTrackingWithIdentityResolution().Include(a=>a.Position).Include(a=>a.socialNetworks).Where(a=>!a.iSDeleted).Select(a=>new AuthorGetDto { FullName=a.FullName,Info=a.Info}).ToList());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = await _context.Positions.Where(x => !x.iSDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorPostDto AuthorDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(!_context.Positions.Any(p=>p.Id==AuthorDto.PositionId))
            {
                ModelState.AddModelError("", "Position invalid");
                return View();
            }

            //Author author = new Author(AuthorDto.FullName, AuthorDto.Info, AuthorDto.PositionId);

            Author author=new Author
            {
                FullName=AuthorDto.FullName,
                Info=AuthorDto.Info,
                PositionId=AuthorDto.PositionId
            };

            for (int i = 0; i < AuthorDto.Icons.Count(); i++)
            {
                SocialNetwork socialNetwork = new SocialNetwork();
                socialNetwork.Icon = AuthorDto.Icons[i];
                socialNetwork.Url = AuthorDto.Urls[i];
                //author.socialNetworks.Add(socialNetwork);
                //socialNetwork.AuthorId = author.Id;
                socialNetwork.Author= author;
            }

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
