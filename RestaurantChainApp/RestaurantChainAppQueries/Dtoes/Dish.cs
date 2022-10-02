using System;

namespace RestaurantChainAppQueries.Dtoes
{
    public class Dish : Dto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool IsMeal { get; set; }
        public string ImgUrl { get; set; }
    }
}
