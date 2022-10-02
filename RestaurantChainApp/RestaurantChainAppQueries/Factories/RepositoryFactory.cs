using RestaurantChainAppQueries.Repositories;

namespace RestaurantChainAppQueries.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public DishesRepository CreateDishesRepository() 
        {
            return new DishesRepository();
        }

        public OrderDtoRepository CreateOrderDtoRepository() 
        {
            return new OrderDtoRepository();
        }
    }
}
