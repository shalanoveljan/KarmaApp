using Karma.Core.DTOS;
using Karma.Core.Repositories;
using Karma.Service.Exceptions;
using Karma.Service.Services.Interfaces;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Services.Implementations
{
    public class ProductImageService : IProductImageService
    {
        readonly IProductImageRepository productImageRepository;

        public ProductImageService(IProductImageRepository productImageRepository)
        {
            this.productImageRepository = productImageRepository;
        }

        public async Task RemoveAsync(int id)
        {
            var image = await productImageRepository.GetAsync(x => x.Id == id && x.iSDeleted == false);

            if (image == null)
            {
                throw new ItemNotFoundException("The image not found");
            }

            image.iSDeleted = true;
            await productImageRepository.UpdateAsync(image);
            await productImageRepository.SaveChangesAsync();
        }
    }
}
