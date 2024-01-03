using Karma.Core.DTOS;
using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Data.Repositories;
using Karma.Service.Exceptions;
using Karma.Service.Extensions;
using Karma.Service.Responses;
using Karma.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Services.Implementations
{
    public class AuthorService : IAuthorService
    {

        readonly IAuthorRepository _authorRepository;
        readonly IPositionRepository _positionRepository;
        readonly IWebHostEnvironment _env;
        public AuthorService(IPositionRepository positionRepository, IWebHostEnvironment env, IAuthorRepository authorRepository)
        {
            _positionRepository = positionRepository;
            _env = env;
            _authorRepository = authorRepository;
        }



        public async Task<CommonResponse> CreateAsync(AuthorPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200
            };

            if (dto.ImageFile == null)
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The field Image is required";
                return commonResponse;
            }

            if(dto.Icons==null || dto.Urls==null || dto.Icons.Count()==dto.Urls.Count()) {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The social network is not valid";
                return commonResponse;
            }

            if (dto.Icons.Any(x=>string.IsNullOrWhiteSpace(x)) || dto.Urls.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The social network is not valid";
                return commonResponse;
            }
            if (!await CheckPosition(dto.PositionId))
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "The Position is not valid";
                return commonResponse;
            }

            Author Author = new Author();
            Author.FullName = dto.FullName;
            Author.PositionId = dto.PositionId;
            Author.Info = dto.Info;
            Author.socialNetworks = new List<SocialNetwork>();


            CreateSocial(dto,Author);

            //if (!dto.ImageFile.ContentType.Contains("image"))
            //{
            //    commonResponse.StatusCode = 400;
            //    commonResponse.Message = "image is not valid";
            //    return commonResponse;
            //} // IsImage birbasa service de yazilis sekli 

            Author.Storage = "wwwroot";
            if (!dto.ImageFile.IsImage())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "image is not valid";
                return commonResponse;

            }
            if (!dto.ImageFile.IsSizeOk(1))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "image size is not valid";
                return commonResponse;

            }
            Author.Image = dto.ImageFile.SaveFile(_env.WebRootPath,"img/author");

            await _authorRepository.AddAsync(Author);
            await _authorRepository.SaveChangesAsync();
            return commonResponse;
        }

        private void CreateSocial(AuthorPostDto dto,Author author)
        {
            for (int i = 0; i < dto.Icons.Count(); i++)
            {
                SocialNetwork socialNetwork = new SocialNetwork
                {
                    Author = author,
                    Icon = dto.Icons[i],
                    Url = dto.Urls[i]
                };
                author.socialNetworks.Add(socialNetwork);
            }
        }

        public async Task<PagginatedResponse<AuthorGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<AuthorGetDto> pagginatedResponse = new PagginatedResponse<AuthorGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _authorRepository.GetQuery(x => !x.iSDeleted)
               .AsNoTrackingWithIdentityResolution()
               .Include(x => x.Position);
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
               .Take(3)
               .Select(x =>
               new AuthorGetDto
               {
                   FullName = x.FullName,
                   Id = x.Id,
                   position = new PositionGetDto { PositionName = x.Position.PositionName },
                   Image = x.Image
               })
               .ToListAsync();

            return pagginatedResponse;
        }

        public async Task<AuthorGetDto> GetAsync(int id)
        {
            Author? Author = await _authorRepository.GetAsync(x => !x.iSDeleted && x.Id == id, "Position", "SocialNetworks");

            if (Author == null)
            {
                throw new ItemNotFoundException("Author not Found");
            }

            AuthorGetDto AuthorGetDto = new AuthorGetDto
            {
                Id = Author.Id,
                FullName = Author.FullName,
                Info = Author.Info,
                Icons = Author.socialNetworks.Select(x => x.Icon).ToList(),
                Urls = Author.socialNetworks.Select(x => x.Url).ToList(),
                PositionId = Author.PositionId,
                position = new PositionGetDto { PositionName = Author.Position.PositionName },
                Image=Author.Image
                
            };

            return AuthorGetDto;


        }

        public async Task RemoveAsync(int id)
        {
            Author? Author = await _authorRepository.GetAsync(x => !x.iSDeleted && x.Id == id);

            if (Author == null)
            {
                throw new ItemNotFoundException("Author not Found");
            }

            Author.iSDeleted = true;
            await _authorRepository.UpdateAsync(Author);
            await _authorRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, AuthorPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200
            };

            if (dto.Icons == null || dto.Urls == null || dto.Icons.Count() == dto.Urls.Count())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The social network is not valid";
                return commonResponse;
            }

            if (dto.Icons.Any(x => string.IsNullOrWhiteSpace(x)) || dto.Urls.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The social network is not valid";
                return commonResponse;
            }
            if (!await CheckPosition(dto.PositionId))
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "The Position is not valid";
                return commonResponse;
            }

            Author? Author = await _authorRepository.GetAsync(x => !x.iSDeleted && x.Id == id, "SocialNetworks");

            if (Author == null)
            {
                throw new ItemNotFoundException("Author Not Found");
            }

            Author.FullName = dto.FullName;
            Author.Info = dto.Info;
            Author.PositionId = dto.PositionId;
            Author.socialNetworks.Clear();

            CreateSocial(dto, Author);

            if(dto.ImageFile!= null)
            {
                if (!dto.ImageFile.IsImage())
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "image is not valid";
                    return commonResponse;

                }
                if (!dto.ImageFile.IsSizeOk(1))
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "image size is not valid";
                    return commonResponse;
                }

                //Helper.RemoveFile(_env.WebRootPath, "img/author", Author.Image);//evvelkini silir yenisini null-in uzerine update edir sanki

                Author.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "img/author");
            }

            await _authorRepository.UpdateAsync(Author);
            await _authorRepository.SaveChangesAsync();
            return commonResponse;
        }

        private async Task<bool> CheckPosition(int id)
        {
            return await _positionRepository.GetQuery(x => !x.iSDeleted && x.Id == id).CountAsync() > 0;
        }
    }
}
