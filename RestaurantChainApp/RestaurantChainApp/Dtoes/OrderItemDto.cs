namespace RestaurantChainApp.Dtoes
{
    public class OrderItemDto : Dto
    {
        public long OrderId { get; set; }
        public long MenuItemId { get; set; }
        public int Amount { get; set; }
        public double Value { get; set; }
    }
}
