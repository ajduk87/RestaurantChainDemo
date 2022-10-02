using RestaurantChainAppQueries.Repositories;

namespace RestaurantChainAppQueries.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public DishesRepository CreateDishesRepository() 
        {
            return new DishesRepository();
        }
    }
}
