using RestaurantChainApp.Dtoes;
using System.Linq;
using System;

namespace RestaurantChainApp.BusinessLogic.CalculationPriceStrategies
{
    public class MealPriceStrategy : CalculationPriceStrategy
    {
        public override double Calculate(Dish dish)
        {
            Meal meal = (Meal)dish;
            return Math.Round(0.9 * meal.Dishes.Sum(dish => dish.Price), 2);
        }
    }
}
