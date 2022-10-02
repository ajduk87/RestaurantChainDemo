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

        private readonly IHappyHourCalculator happyHourCalculator;

        public PriceCalculator(IHappyHourCalculator happyHourCalculator)
        {
            dishPriceStrategy = new DishPriceStrategy();
            mealPriceStrategy = new MealPriceStrategy();

            this.happyHourCalculator = happyHourCalculator;
        }      

        private double CalculateForMeal(Meal meal, int hourForCalculation, int happyHourBegin, int happyHourEnd) 
        {
            bool isHappyHour = happyHourCalculator.IsHappyHour(currentHour: hourForCalculation, happyHourBegin, happyHourEnd);

            return isHappyHour ? Math.Round(0.8 * mealPriceStrategy.Calculate(meal), 2) :
                                   Math.Round(mealPriceStrategy.Calculate(meal), 2);
        }

        private double CalculateForDish(Dish dish, int hourForCalculation, int happyHourBegin, int happyHourEnd)
        {
            bool isHappyHour = happyHourCalculator.IsHappyHour(currentHour: hourForCalculation, happyHourBegin, happyHourEnd);

            return isHappyHour ? Math.Round(0.8 * dishPriceStrategy.Calculate(dish) , 2):
                                   Math.Round(dishPriceStrategy.Calculate(dish) , 2);
        }

        public List<Meal> CalculateForMeals(List<Meal> meals, int hourForCalculation, int happyHourBegin, int happyHourEnd) 
        {
            List<Meal> newMeals = new List<Meal>();
            foreach (var meal in meals) 
            {
                meal.Price = CalculateForMeal(meal, hourForCalculation, happyHourBegin, happyHourEnd);
                newMeals.Add(meal);
            }

            return newMeals;
        }

        public List<Dish> CalculateForDishes(List<Dish> dishes, int hourForCalculation, int happyHourBegin, int happyHourEnd) 
        {
            List<Dish> newDishes = new List<Dish>();
            foreach (var dish in dishes)
            {
                dish.Price = CalculateForDish(dish, hourForCalculation, happyHourBegin, happyHourEnd);
                newDishes.Add(dish);
            }

            return newDishes;
        }
    }
}
