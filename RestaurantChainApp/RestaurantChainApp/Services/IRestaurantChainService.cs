﻿using RestaurantChainApp.Dtoes;
using System.Collections.Generic;

namespace RestaurantChainApp.Services
{
    public interface IRestaurantChainService
    {
        void UpdateMenu();
        //List<Dish> GetSingleDishes();
        //List<Meal> GetMeals();
        
        //OrderDto GetOrder(int orderid);
        void CreateOrder(OrderDto orderDto);
        void ModifyOrder(OrderDto orderDto);
        //HappyHour HappyHour();
    }
}
