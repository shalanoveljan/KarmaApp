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
    public class TagService : ITagService
    {

        readonly ITagRepository _tagRepository;

        public TagService(ITagRepository TagRepository)
        {
            _tagRepository = TagRepository;
        }

        public async Task CreateAsync(TagPostDto dto)
        {
            Tag Tag = new Tag();
            Tag.Name = dto.TagName;
            await _tagRepository.AddAsync(Tag);
            await _tagRepository.SaveChangesAsync();
        }

        public async Task<PagginatedResponse<TagGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<TagGetDto> pagginatedResponse = new PagginatedResponse<TagGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _tagRepository.GetQuery(x => !x.iSDeleted)
                .AsNoTrackingWithIdentityResolution();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new TagGetDto { TagName = x.Name, Id = x.Id, CreatedAt = x.CreateAt })
                .ToListAsync();


            return pagginatedResponse;
        }

        public async Task<TagGetDto> GetAsync(int id)
        {
            Tag? Tag = await _tagRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Tag == null)
            {
                throw new ItemNotFoundException("Tag not Found");
            }

            TagGetDto TagGetDto=new TagGetDto
            {
            Id = Tag.Id,
            TagName=Tag.Name,
            CreatedAt= Tag.CreateAt
            };

            return TagGetDto;


        }

        public async Task RemoveAsync(int id)
        {
            Tag? Tag = await _tagRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Tag == null)
            {
                throw new ItemNotFoundException("Tag not Found");
            }

            Tag.iSDeleted = true;
            await _tagRepository.UpdateAsync(Tag);
            await _tagRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, TagPostDto dto)
        {
            Tag? Tag = await _tagRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Tag == null)
            {
                throw new ItemNotFoundException("Tag not Found");
            }

            //if (!(await _tagRepository.AnyAsync(x => x.Id == id)))
            //{
            //    ModelState.AddModelError("Id", "Agilli ol");
            //    return View();
            //}

            Tag.Name = dto.TagName;
            await _tagRepository.UpdateAsync(Tag);
            await _tagRepository.SaveChangesAsync();
        }
    }
}
