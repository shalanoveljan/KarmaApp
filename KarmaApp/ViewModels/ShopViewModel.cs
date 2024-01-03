using Karma.Core.DTOS;
using Karma.Service.Responses;

namespace KarmaApp.ViewModels
{
    public class ShopViewModel
    {
        public PagginatedResponse<CategoryGetDto> categories { get; set; } = null!;
        public PagginatedResponse<BrandGetDto> brands { get; set; } = null!;
        public PagginatedResponse<ColorGetDto> colors { get; set; } = null!;
        public PagginatedResponse<ProductGetDto> Products { get; set; } = null!;


    }
}
