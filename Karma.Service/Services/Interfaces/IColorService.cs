using Karma.Core.DTOS;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Services.Interfaces
{
    public interface IColorService
    {
        public Task<PagginatedResponse<ColorGetDto>> GetAllAsync(int page=1);
        public Task CreateAsync(ColorPostDto dto);

        public Task RemoveAsync(int id);
        public Task UpdateAsync(int id, ColorPostDto dto);

        public Task<ColorGetDto> GetAsync(int id);//GetById
    }
}
