using Karma.Core.DTOS;
using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Data.Repositories;
using Karma.Service.Exceptions;
using Karma.Service.Responses;
using Karma.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Services.Implementations
{
    public class ColorService : IColorService
    {

        readonly IColorRepository _colorRepository;

        public ColorService(IColorRepository ColorRepository)
        {
            _colorRepository = ColorRepository;
        }

        public async Task CreateAsync(ColorPostDto dto)
        {
            Color Color = new Color();
            Color.ColorName = dto.Name;
            await _colorRepository.AddAsync(Color);
            await _colorRepository.SaveChangesAsync();
        }
        public async Task<PagginatedResponse<ColorGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<ColorGetDto> pagginatedResponse = new PagginatedResponse<ColorGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _colorRepository.GetQuery(x => !x.iSDeleted)
                .AsNoTrackingWithIdentityResolution();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new ColorGetDto { ColorName = x.ColorName, Id = x.Id, CreatedAt = x.CreateAt, ProductCount = x.productColors.Where(x => !x.iSDeleted).Count() })
                .ToListAsync();


            return pagginatedResponse;
        }
        public async Task<ColorGetDto> GetAsync(int id)
        {
            Color? Color = await _colorRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Color == null)
            {
                throw new ItemNotFoundException("Color not Found");
            }

            ColorGetDto ColorGetDto=new ColorGetDto
            {
            Id = Color.Id,
            ColorName=Color.ColorName,
            CreatedAt= Color.CreateAt
            };

            return ColorGetDto;


        }

        public async Task RemoveAsync(int id)
        {
            Color? Color = await _colorRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Color == null)
            {
                throw new ItemNotFoundException("Color not Found");
            }

            Color.iSDeleted = true;
            await _colorRepository.UpdateAsync(Color);
            await _colorRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ColorPostDto dto)
        {
            Color? Color = await _colorRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Color == null)
            {
                throw new ItemNotFoundException("Color not Found");
            }

            //if (!(await _colorRepository.AnyAsync(x => x.Id == id)))
            //{
            //    ModelState.AddModelError("Id", "Agilli ol");
            //    return View();
            //}

            Color.ColorName= dto.Name;
            await _colorRepository.UpdateAsync(Color);
            await _colorRepository.SaveChangesAsync();
        }
    }
}
