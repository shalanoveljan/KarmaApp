using Karma.Core.DTOS;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Services.Interfaces
{
    public interface IPositionService
    {
        public Task<PagginatedResponse<PositionGetDto>> GetAllAsync(int page=1);
        public Task CreateAsync(PositionPostDto dto);

        public Task RemoveAsync(int id);
        public Task UpdateAsync(int id, PositionPostDto dto);

        public Task<PositionGetDto> GetAsync(int id);//GetById
    }
}
