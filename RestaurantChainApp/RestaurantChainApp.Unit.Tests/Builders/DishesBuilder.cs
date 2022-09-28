using RestaurantChainApp.Dtoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantChainApp.Unit.Tests.Builders
{
    public class DishesBuilder
    {
        public List<Dish> Build() 
        {
            List<Dish> dishes = new List<Dish>();

            Dish dish = new Dish 
            {
                Name = "Veal soup",
                Description = "Soup made from veal joint",
                Price = 3.54,
                IsMeal = false
            };

            dishes.Add(dish);

            return dishes;
        }
    }
}
