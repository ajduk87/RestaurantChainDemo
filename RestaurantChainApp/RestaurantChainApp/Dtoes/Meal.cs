using System;
using System.Collections.Generic;

namespace RestaurantChainApp.Dtoes
{
    public class Meal : Dish
    {
        public List<Dish> Dishes { get; set; }

        public void AddDish(Dish dish)
        {
            Dishes = Dishes ?? new List<Dish>();
            Dishes.Add(dish);
        }       
    }
}
