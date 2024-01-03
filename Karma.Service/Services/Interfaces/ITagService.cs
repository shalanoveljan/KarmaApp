using Karma.Core.DTOS;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Services.Interfaces
{
    public interface ITagService
    {
        public Task<PagginatedResponse<TagGetDto>> GetAllAsync(int page=1);
        public Task CreateAsync(TagPostDto dto);

        public Task RemoveAsync(int id);
        public Task UpdateAsync(int id, TagPostDto dto);

        public Task<TagGetDto> GetAsync(int id);//GetById
    }
}
