using RestaurantChainApp.Dtoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantChainApp.Unit.Tests.Builders
{
    public class MealsBuilder
    {
        public List<Meal> Build()
        {
            List<Meal> meals = new List<Meal>();

            List<Dish> dishes = new List<Dish>();

            dishes.Add(new Dish 
            {
                Name = "Chicken soup",
                Description = "Soup made from chicken back",
                Price = 2.76,
                IsMeal = false
            });

            dishes.Add(new Dish
            {
                Name = "Crispy chicken",
                Description = "Chicken steak with almonds, walnuts and sour apple sauce",
                Price = 7.87,
                IsMeal = false
            });

            dishes.Add(new Dish
            {
                Name = "Tomato salad",
                Description = "Salad of chopped tomatoes on rings seasoned with olive oil",
                Price = 2.59,
                IsMeal = false
            });

            dishes.Add(new Dish
            {
                Name = "White bread",
                Description = "Bread made from wheat",
                Price = 1.34,
                IsMeal = false
            });

            Meal meal = new Meal
            {
                Name = "Spring meal",
                Description = "Chicken soup and Crispy chicken with Tomato salad and White bread",
                IsMeal = true,
                Dishes = dishes
            };

            meals.Add(meal);

            return meals;
        }
    }
}
