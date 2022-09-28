using RestaurantChainApp.BusinessLogic.CalculationPriceStrategies;
using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Entities;
using RestaurantChainApp.Factories;
using System;
using System.Collections.Generic;

namespace RestaurantChainApp.BusinessLogic
{
    public class PriceCalculator : IPriceCalculator
    {
      
        private readonly CalculationPriceStrategy dishPriceStrategy;
        private readonly CalculationPriceStrategy mealPriceStrategy;

        public PriceCalculator()
        {
            dishPriceStrategy = new DishPriceStrategy();
            mealPriceStrategy = new MealPriceStrategy();
        }      

        private double CalculateForMeal(Meal meal, bool isHappyHour) 
        {
            return isHappyHour ? Math.Round(0.8 * mealPriceStrategy.Calculate(meal), 2) :
                                   Math.Round(mealPriceStrategy.Calculate(meal), 2);
        }

        private double CalculateForDish(Dish dish, bool isHappyHour)
        {
            return isHappyHour ? Math.Round(0.8 * dishPriceStrategy.Calculate(dish) , 2):
                                   Math.Round(dishPriceStrategy.Calculate(dish) , 2);
        }

        public List<Meal> CalculateForMeals(List<Meal> meals, bool isHappyHour) 
        {
            List<Meal> newMeals = new List<Meal>();
            foreach (var meal in meals) 
            {
                meal.Price = CalculateForMeal(meal, isHappyHour);
                newMeals.Add(meal);
            }

            return newMeals;
        }

        public List<Dish> CalculateForDishes(List<Dish> dishes, bool isHappyHour) 
        {
            List<Dish> newDishes = new List<Dish>();
            foreach (var dish in dishes)
            {
                dish.Price = CalculateForDish(dish, isHappyHour);
                newDishes.Add(dish);
            }

            return newDishes;
        }
    }
}
