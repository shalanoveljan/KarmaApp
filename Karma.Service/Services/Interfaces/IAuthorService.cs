using Karma.Core.DTOS;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Services.Interfaces
{
    public interface IAuthorService
    {
        public Task<PagginatedResponse<AuthorGetDto>> GetAllAsync(int page=1);
        public Task<CommonResponse> CreateAsync(AuthorPostDto dto);

        public Task RemoveAsync(int id);
        public Task<CommonResponse> UpdateAsync(int id, AuthorPostDto dto);

        public Task<AuthorGetDto> GetAsync(int id);//GetById
    }
}
