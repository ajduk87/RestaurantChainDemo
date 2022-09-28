using Newtonsoft.Json;
using NUnit.Framework;
using RestaurantChainApp.Dtoes;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace RestaurantChainApp.Integration.Tests
{
    public class RestaurantChainTests
    {
        private ApiCaller apiCaller;

        [SetUp]
        public void Setup()
        {     
            apiCaller = new ApiCaller();
        }

        [Test]
        public void GetMenuTest()
        {
            string response = this.apiCaller.Get(Urls.GetMenuUrl());

            List<Dish> dishes = JsonConvert.DeserializeObject<List<Dish>>(response);

            Assert.That(dishes.Count, Is.EqualTo(14));
            Assert.That(dishes.Where(dish => !dish.IsMeal).Count, Is.EqualTo(10));
            Assert.That(dishes.Where(dish => dish.IsMeal).Count, Is.EqualTo(4));
        }

        [Test]
        public void GetSingleDishesTest()
        {
            string response = this.apiCaller.Get(Urls.GetSingleDishesUrl());

            List<Dish> dishes = JsonConvert.DeserializeObject<List<Dish>>(response);

            Assert.That(dishes.Count, Is.EqualTo(10));
        }

        [Test]
        public void GetMealsTest()
        {
            string response = this.apiCaller.Get(Urls.GetMealsUrl());

            List<Meal> meals = JsonConvert.DeserializeObject<List<Meal>>(response);

            Assert.That(meals.Count, Is.EqualTo(4));
        }

        [Test]
        public void GetOrderTest()
        {
            int orderid = 5;
            string query = $"?orderid={orderid}";
            string response = this.apiCaller.Get(Urls.GetOrderUrl(), query);

            OrderDto order = JsonConvert.DeserializeObject<OrderDto>(response);

            Assert.That(order.Total, Is.EqualTo(1.05));
            Assert.That(order.orderItems.Count, Is.EqualTo(3));
        }

        [Test]
        public void CreateOrderTest()
        {
            OrderDto order = new OrderDto();
            RestResponse response = apiCaller.Post(Urls.CreateOrderUrl(), order);


            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void ModifyOrderTest()
        {
            OrderDto order = new OrderDto();
            RestResponse response = apiCaller.Put(Urls.CreateOrderUrl(), order);


            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}