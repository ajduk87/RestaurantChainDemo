using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantChainApp.Integration.Tests
{
    public static class Urls
    {
        private static readonly string ServerIpAddress = "https://localhost:5001/";

        public static string GetMenuUrl()
        {
            return $"{ServerIpAddress}RestaurantChain/GetMenu";
        }

        public static string GetSingleDishesUrl()
        {
            return $"{ServerIpAddress}RestaurantChain/GetSingleDishes";
        }

        public static string GetMealsUrl()
        {
            return $"{ServerIpAddress}RestaurantChain/GetMeals";
        }

        public static string HappyHourUrl()
        {
            return $"{ServerIpAddress}RestaurantChain/HappyHour";
        }

        public static string GetOrderUrl()
        {
            return $"{ServerIpAddress}RestaurantChain/GetOrder";
        }

        public static string CreateOrderUrl()
        {
            return $"{ServerIpAddress}RestaurantChain/CreateOrder";
        }

        public static string ModifyOrderUrl()
        {
            return $"{ServerIpAddress}RestaurantChain/ModifyOrder";
        }
    }
}
