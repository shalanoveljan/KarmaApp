using Karma.Core.DTOS;

namespace KarmaApp.ViewModels
{
    public class ShopViewModel
    {
        public IEnumerable<CategoryGetDto> categories { get; set; }
        public IEnumerable<BrandGetDto> brands { get; set; }
        public IEnumerable<ColorGetDto> colors { get; set; }


    }
}
