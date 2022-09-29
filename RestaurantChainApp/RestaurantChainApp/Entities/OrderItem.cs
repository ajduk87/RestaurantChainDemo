namespace RestaurantChainApp.Entities
{
    public class OrderItem : Entity
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Amount { get; set; }
        public double Value { get; set; }
    }
}
