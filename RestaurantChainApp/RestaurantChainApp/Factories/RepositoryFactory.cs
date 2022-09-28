using RestaurantChainApp.Repositories;

namespace RestaurantChainApp.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {     

        public MenuItemsRepository CreateMenuItemsRepository() 
        {
            return new MenuItemsRepository();
        }

        public OrderItemsRepository CreateOrderItemsRepository() 
        {
            return new OrderItemsRepository();
        }

        public OrdersRepository CreateOrdersRepository() 
        {
            return new OrdersRepository();
        }
    }
}
