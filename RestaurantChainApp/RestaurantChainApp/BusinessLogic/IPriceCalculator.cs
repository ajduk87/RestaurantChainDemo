using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Entities;
using System.Collections.Generic;

namespace RestaurantChainApp.BusinessLogic
{
    public interface IPriceCalculator
    {
        List<Meal> CalculateForMeals(List<Meal> meals, int hourForCalculation, int happyHourBegin,int happyHourEnd);
        List<Dish> CalculateForDishes(List<Dish> dishes, int hourForCalculation, int happyHourBegin,int happyHourEnd);
    }
}
