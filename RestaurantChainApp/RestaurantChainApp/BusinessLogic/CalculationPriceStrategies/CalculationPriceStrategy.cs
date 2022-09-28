using RestaurantChainApp.Dtoes;

namespace RestaurantChainApp.BusinessLogic.CalculationPriceStrategies
{
    public abstract class CalculationPriceStrategy
    {
        public abstract double Calculate(Dish dish);
    }
}
