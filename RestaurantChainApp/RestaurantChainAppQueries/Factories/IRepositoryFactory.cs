using RestaurantChainAppQueries.Repositories;

namespace RestaurantChainAppQueries.Factories
{
    public interface IRepositoryFactory
    {
        DishesRepository CreateDishesRepository();
        OrderDtoRepository CreateOrderDtoRepository();
    }
}
