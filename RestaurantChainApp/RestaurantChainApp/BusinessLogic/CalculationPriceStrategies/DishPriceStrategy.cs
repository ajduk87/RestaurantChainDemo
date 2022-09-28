using RestaurantChainApp.Dtoes;

namespace RestaurantChainApp.BusinessLogic.CalculationPriceStrategies
{
    public class DishPriceStrategy : CalculationPriceStrategy
    {
        public override double Calculate(Dish dish) 
        {
            return dish.Price;
        }
    }
}
