using Karma.Core.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Services.Interfaces
{
    public interface IBasketService
    {
        public Task AddBasket(int id, int? count);

        public Task<BasketGetDto> GetBasket();
    }
}
