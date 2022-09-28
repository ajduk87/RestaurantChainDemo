namespace RestaurantChainApp.Entities
{
    public class MenuItem : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool IsMeal { get; set; }
        public string ImgUrl { get; set; }
    }
}
