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
    public class CategoryService : ICategoryService
    {

        readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(CategoryPostDto dto)
        {
            Category category = new Category();
            category.CategoryName = dto.CategoryName;
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<PagginatedResponse<CategoryGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<CategoryGetDto> pagginatedResponse = new PagginatedResponse<CategoryGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _categoryRepository.GetQuery(x => !x.iSDeleted)
                .AsNoTrackingWithIdentityResolution();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new CategoryGetDto {CategoryName  = x.CategoryName, Id = x.Id, CreatedAt = x.CreateAt, ProductCount = x.Products.Where(x => !x.iSDeleted).Count() })
                .ToListAsync();


            return pagginatedResponse;
        }

        public async Task<CategoryGetDto> GetAsync(int id)
        {
            Category? Category = await _categoryRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Category == null)
            {
                throw new ItemNotFoundException("Category not Found");
            }

            CategoryGetDto categoryGetDto=new CategoryGetDto
            {
            Id = Category.Id,
            CategoryName=Category.CategoryName,
            CreatedAt= Category.CreateAt
            };

            return categoryGetDto;


        }

        public async Task RemoveAsync(int id)
        {
            Category? Category = await _categoryRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Category == null)
            {
                throw new ItemNotFoundException("Category not Found");
            }

            Category.iSDeleted = true;
            await _categoryRepository.UpdateAsync(Category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CategoryPostDto dto)
        {
            Category? Category = await _categoryRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Category == null)
            {
                throw new ItemNotFoundException("Category not Found");
            }

            //if (!(await _CategoryRepository.AnyAsync(x => x.Id == id)))
            //{
            //    ModelState.AddModelError("Id", "Agilli ol");
            //    return View();
            //}

            Category.CategoryName= dto.CategoryName;
            await _categoryRepository.UpdateAsync(Category);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
