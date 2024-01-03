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
    public class BrandService : IBrandService
    {

        readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository BrandRepository)
        {
            _brandRepository = BrandRepository;
        }

        public async Task CreateAsync(BrandPostDto dto)
        {
            Brand Brand = new Brand();
            Brand.Name = dto.BrandName;
            await _brandRepository.AddAsync(Brand);
            await _brandRepository.SaveChangesAsync();
        }

        public async Task<PagginatedResponse<BrandGetDto>> GetAllAsync(int page=1)
        {
            PagginatedResponse<BrandGetDto> pagginatedResponse = new PagginatedResponse<BrandGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _brandRepository.GetQuery(x => !x.iSDeleted)
                .AsNoTrackingWithIdentityResolution();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items=await query.Skip((page-1)*3)
                .Take(3)
                 .Select(x => new BrandGetDto { BrandName = x.Name, Id = x.Id, CreatedAt = x.CreateAt, ProductCount = x.Products.Where(x => !x.iSDeleted).Count() })
                .ToListAsync();


            return pagginatedResponse;
        }

        public async Task<BrandGetDto> GetAsync(int id)
        {
            Brand? Brand = await _brandRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Brand == null)
            {
                throw new ItemNotFoundException("Brand not Found");
            }

            BrandGetDto BrandGetDto=new BrandGetDto
            {
            Id = Brand.Id,
            BrandName=Brand.Name,
            CreatedAt= Brand.CreateAt
            };

            return BrandGetDto;


        }

        public async Task RemoveAsync(int id)
        {
            Brand? Brand = await _brandRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Brand == null)
            {
                throw new ItemNotFoundException("Brand not Found");
            }

            Brand.iSDeleted = true;
            await _brandRepository.UpdateAsync(Brand);
            await _brandRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, BrandPostDto dto)
        {
            Brand? Brand = await _brandRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Brand == null)
            {
                throw new ItemNotFoundException("Brand not Found");
            }

            //if (!(await _brandRepository.AnyAsync(x => x.Id == id)))
            //{
            //    ModelState.AddModelError("Id", "Agilli ol");
            //    return View();
            //}

            Brand.Name = dto.BrandName;
            await _brandRepository.UpdateAsync(Brand);
            await _brandRepository.SaveChangesAsync();
        }
    }
}
