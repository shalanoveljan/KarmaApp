using Karma.Core.DTOS;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Services.Interfaces
{
    public interface IProductService
    {
        public Task<PagginatedResponse<ProductGetDto>> GetAllAsync(int page=1);

        public Task<CommonResponse> CreateAsync(ProductPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, ProductPostDto dto);
        public Task<ProductGetDto> GetAsync(int id);
    }
}
