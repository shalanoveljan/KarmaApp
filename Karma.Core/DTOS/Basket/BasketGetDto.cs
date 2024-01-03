using System;
namespace Karma.Core.DTOS
{
	public class BasketGetDto
	{
        public List<BasketItem> basketItems { get; set; }
        public double TotalPrice { get; set; }
        public BasketGetDto()
        {
            basketItems= new List<BasketItem>();
        }
	}

	public class BasketItem
	{
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; }= null!;
        public double Price { get; set; }
        public int Count { get; set; }
    }
}

