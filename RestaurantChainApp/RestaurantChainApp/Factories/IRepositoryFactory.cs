using RestaurantChainApp.Repositories;

namespace RestaurantChainApp.Factories
{
    public interface IRepositoryFactory
    {
        MenuItemsRepository CreateMenuItemsRepository();
        OrderItemsRepository CreateOrderItemsRepository();
        OrdersRepository CreateOrdersRepository();
    }
}
