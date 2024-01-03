using Karma.Core.DTOS;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Services.Interfaces
{
    public interface IBrandService
    {
        public Task<PagginatedResponse<BrandGetDto>> GetAllAsync(int page=1);
        public Task CreateAsync(BrandPostDto dto);

        public Task RemoveAsync(int id);
        public Task UpdateAsync(int id, BrandPostDto dto);

        public Task<BrandGetDto> GetAsync(int id);//GetById
    }
}
