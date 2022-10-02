namespace RestaurantChainAppQueries.Dtoes
{
    public class OrderItemDto : Dto
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Amount { get; set; }
        public double Value { get; set; }
    }
}
