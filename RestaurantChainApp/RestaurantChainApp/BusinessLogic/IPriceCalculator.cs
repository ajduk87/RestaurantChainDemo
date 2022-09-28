using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Entities;
using System.Collections.Generic;

namespace RestaurantChainApp.BusinessLogic
{
    public interface IPriceCalculator
    {
        List<Meal> CalculateForMeals(List<Meal> meals, bool isHappyHour);
        List<Dish> CalculateForDishes(List<Dish> dishes, bool isHappyHour);
    }
}
