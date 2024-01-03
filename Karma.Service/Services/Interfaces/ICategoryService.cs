using Karma.Core.DTOS;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<PagginatedResponse<CategoryGetDto>> GetAllAsync(int page=1);
        public Task CreateAsync(CategoryPostDto dto);

        public Task RemoveAsync(int id);
        public Task UpdateAsync(int id,CategoryPostDto dto);

        public Task<CategoryGetDto> GetAsync(int id);//GetById





    }
}
