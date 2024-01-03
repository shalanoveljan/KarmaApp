using Karma.Core.DTOS;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Services.Interfaces
{
    public interface IBlogService
    {
        public Task<PagginatedResponse<BlogGetDto>> GetAllAsync(int page=1);
        public Task<CommonResponse> CreateAsync(BlogPostDto dto);

        public Task RemoveAsync(int id);
        public Task<CommonResponse> UpdateAsync(int id, BlogPostDto dto);

        public Task<BlogGetDto> GetAsync(int id);//GetById
    }
}
