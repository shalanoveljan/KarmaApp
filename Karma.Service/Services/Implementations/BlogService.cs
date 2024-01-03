using Karma.Core.DTOS;
using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Data.Repositories;
using Karma.Service.Exceptions;
using Karma.Service.Extensions;
using Karma.Service.Responses;
using Karma.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Services.Implementations
{
    public class BlogService : IBlogService
    {

        readonly IBlogRepository _blogRepository;
        readonly IAuthorRepository _authorRepository;
        readonly IWebHostEnvironment _env;
        public BlogService(IPositionRepository positionRepository, IWebHostEnvironment env, IBlogRepository BlogRepository, IAuthorRepository authorRepository)
        {
            _env = env;
            _blogRepository = BlogRepository;
            _authorRepository = authorRepository;
        }



        public async Task<CommonResponse> CreateAsync(BlogPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;

            Blog blog = new Blog
            {
                AuthorId = dto.AuthorId,
                Description = dto.Description,
                Title = dto.Title,
                tagBlogs = new List<TagBlog>()
            };

            if (!await CheckAuthor(dto.AuthorId))
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "The Position is not valid";
                return commonResponse;
            }

            if (dto.ImageFile == null)
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The field image is required";
                return commonResponse;
            }

            if (!dto.ImageFile.IsImage())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "Image is not valid";
                return commonResponse;
            }

            if (!dto.ImageFile.IsSizeOk(1))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "Image Size is not valid";
                return commonResponse;
            }

            blog.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "img/blog");

            foreach (var item in dto.TagsIds)
            {
                blog.tagBlogs.Add(new TagBlog
                {
                    Blog = blog,
                    TagId = item
                });

            }
            await _blogRepository.AddAsync(blog);
            await _blogRepository.SaveChangesAsync();

            return commonResponse;

        }



        public async Task<PagginatedResponse<BlogGetDto>> GetAllAsync(int page=1)
        {
            PagginatedResponse<BlogGetDto> pagginatedResponse=new PagginatedResponse<BlogGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _blogRepository.GetQuery(x => !x.iSDeleted)
             .AsNoTrackingWithIdentityResolution()
             .Include(x => x.tagBlogs)
             .ThenInclude(x => x.Tag)
             .Include(x => x.Author)
              .ThenInclude(x => x.Position);
            
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                  .Select(x =>
              new BlogGetDto
              {
                  Title = x.Title,
                  Id = x.Id,
                  Description = x.Description,
                  tagGetDtos = x.tagBlogs.Select(y => new TagGetDto { TagName = y.Tag.Name, Id = y.TagId }),
                  Image = x.Image,
                  ViewCount = x.ViewCount,
                  Date = x.CreateAt,
                  authorGetDto = new AuthorGetDto { FullName = x.Author.FullName, Image = x.Author.Image },
              })
                .ToListAsync();
            return pagginatedResponse;
        }

        public async Task<BlogGetDto> GetAsync(int id)
        {
            Blog? blog = await _blogRepository.GetAsync(x => !x.iSDeleted && x.Id == id, "tagBlogs.Tag", "Author.Position", "Author.socialNetworks");
            if (blog == null)
            {
                throw new ItemNotFoundException("Blog not found");
            }

            BlogGetDto blogGetDto = new BlogGetDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                Image = blog.Image,
                tagGetDtos = blog.tagBlogs.Select(x => new TagGetDto { TagName = x.Tag.Name, Id = x.TagId }),
                Date = blog.CreateAt,
                ViewCount = blog.ViewCount,
                authorGetDto = new AuthorGetDto { FullName = blog.Author.FullName },
                position=new PositionGetDto { PositionName=blog.Author.Position.PositionName},
                Icons=blog.Author.socialNetworks.Select(x=>x.Icon).ToList(),
                Urls=blog.Author.socialNetworks.Select(x=>x.Url).ToList()

            };
            return blogGetDto;

        }

        public async Task RemoveAsync(int id)
        {
             Blog blog = await _blogRepository.GetAsync(x => !x.iSDeleted && x.Id == id, "TagBlogs.Tag", "Author.Position");
            if (blog == null)
            {
                throw new ItemNotFoundException("Blog not found");
            }
            blog.iSDeleted = true;
            await _blogRepository.UpdateAsync(blog);
            await _blogRepository.SaveChangesAsync();
    }
        

        public async Task<CommonResponse> UpdateAsync(int id, BlogPostDto dto)
        {
        CommonResponse commonResponse = new CommonResponse();
        commonResponse.StatusCode = 200;

        Blog blog = new Blog
        {
            AuthorId = dto.AuthorId,
            Description = dto.Description,
            Title = dto.Title,
            tagBlogs = new List<TagBlog>()
        };

        if (!await CheckAuthor(dto.AuthorId))
        {
            commonResponse.StatusCode = 404;
            commonResponse.Message = "The Position is not valid";
            return commonResponse;
        }

        if (dto.ImageFile == null)
        {
            commonResponse.StatusCode = 400;
            commonResponse.Message = "The field image is required";
                if (!dto.ImageFile.IsImage())
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "Image is not valid";
                    return commonResponse;
                }

                if (!dto.ImageFile.IsSizeOk(1))
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "Image Size is not valid";
                    return commonResponse;
                }

                blog.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "img/blog");
            }

            blog.tagBlogs.Clear();

            foreach (var item in dto.TagsIds)
            {
                blog.tagBlogs.Add(new TagBlog
                {
                    Blog = blog,
                    TagId = item
                });
            }

            await _blogRepository.UpdateAsync(blog);
            await _blogRepository.SaveChangesAsync();
            return commonResponse;

 

    }

    private async Task<bool> CheckAuthor(int id)
        {
            return await _authorRepository.GetQuery(x => !x.iSDeleted && x.Id == id).CountAsync() > 0;
        }
    }
}

