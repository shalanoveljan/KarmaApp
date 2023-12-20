using Karma.Core.DTOS;
using Karma.Core.Entities;
using Karma.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace KarmaApp.Controllers
{
    public class PositionController : Controller
    {

        readonly KarmaDBContext _context;

        public PositionController(KarmaDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<PositionGetDto> positions=_context.Positions.AsNoTracking().Where(p=>!p.iSDeleted).Select(p=> new PositionGetDto { PositionName=p.PositionName}).ToList();   
            return View(positions);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PositionGetDto positionDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist = _context.Positions.Any(p => p.PositionName == positionDto.PositionName);

            //  Category? exist=_context.Categories.FirstOrDefault(x => x.CategoryName.ToLower()==category.CategoryName.ToLower());

            if (isExist)
            {
                ModelState.AddModelError("", $"{positionDto}----Position already exist");
                return View();
            }

            Position NewPosition=new Position();

            NewPosition.PositionName=positionDto.PositionName;

            _context.Positions.Add(NewPosition);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
