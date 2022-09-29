using Newtonsoft.Json;
using NUnit.Framework;
using RestaurantChainApp.Dtoes;
using RestSharp;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;

namespace RestaurantChainApp.Integration.Tests
{
    public class RestaurantChainTests
    {
        private ApiCaller apiCaller;
        private string testDataDirectory;

        [SetUp]
        public void Setup()
        {     
            apiCaller = new ApiCaller();
            string executingAssemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            testDataDirectory = Path.GetFullPath(Path.Combine(executingAssemblyDirectory, @"..\..\..\"));
            testDataDirectory += "TestData\\";
        }

        [Test, Order(1)]
        public void StartApplication()
        {
            string relativeFilePath = @"RestaurantChainApp\bin\Debug\netcoreapp3.1\RestaurantChainApp.exe";
            string executingAssemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string newPath = Path.GetFullPath(Path.Combine(executingAssemblyDirectory, @"..\..\..\..\"));
            string fullFilepath = $"{newPath}{relativeFilePath}";
            Process.Start(fullFilepath);

            Thread.Sleep(3000);

            int restaurantchainappExecutingInstances = Process.GetProcesses().Count(process => process.ProcessName.StartsWith("RestaurantChain"));
            Assert.That(restaurantchainappExecutingInstances, Is.EqualTo(1));
        }

        [Test, Order(2)]
        public void GetMenuTest()
        {
            string response = this.apiCaller.Get(Urls.GetMenuUrl());

            List<Dish> dishes = JsonConvert.DeserializeObject<List<Dish>>(response);

            Assert.That(dishes.Count, Is.EqualTo(14));
            Assert.That(dishes.Where(dish => !dish.IsMeal).Count, Is.EqualTo(10));
            Assert.That(dishes.Where(dish => dish.IsMeal).Count, Is.EqualTo(4));
        }

        [Test, Order(3)]
        public void GetSingleDishesTest()
        {
            string response = this.apiCaller.Get(Urls.GetSingleDishesUrl());

            List<Dish> dishes = JsonConvert.DeserializeObject<List<Dish>>(response);

            Assert.That(dishes.Count, Is.EqualTo(10));
        }

        [Test, Order(4)]
        public void GetMealsTest()
        {
            string response = this.apiCaller.Get(Urls.GetMealsUrl());

            List<Meal> meals = JsonConvert.DeserializeObject<List<Meal>>(response);

            Assert.That(meals.Count, Is.EqualTo(4));
        }
        

        [Test, Order(5)]
        public void CreateOrderTest()
        {
            string fullTestFilePath = $"{testDataDirectory}CreateOrder.json";
            OrderDto order = JsonConvert.DeserializeObject<OrderDto>(File.ReadAllText(fullTestFilePath));
            RestResponse response = apiCaller.Post(Urls.CreateOrderUrl(), order);


            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test, Order(6)]
        public void GetOrderTest()
        {
            int orderid = 1;
            string query = $"?orderid={orderid}";
            string response = this.apiCaller.Get(Urls.GetOrderUrl(), query);

            OrderDto order = JsonConvert.DeserializeObject<OrderDto>(response);

            Assert.That(order.Total, Is.EqualTo(2.62));
            Assert.That(order.orderItems.Count, Is.EqualTo(3));
        }

        [Test, Order(7)]
        public void ModifyOrderTest()
        {
            string fullTestFilePath = $"{testDataDirectory}ModifyOrder.json";
            OrderDto order = JsonConvert.DeserializeObject<OrderDto>(File.ReadAllText(fullTestFilePath));

            RestResponse response = apiCaller.Put(Urls.ModifyOrderUrl(), order);


            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test, Order(8)]
        public void GetModifiedOrderTest()
        {
            int orderid = 2;
            string query = $"?orderid={orderid}";
            string response = this.apiCaller.Get(Urls.GetOrderUrl(), query);

            OrderDto order = JsonConvert.DeserializeObject<OrderDto>(response);

            Assert.That(order.Total, Is.EqualTo(2.99));
            Assert.That(order.orderItems.Count, Is.EqualTo(4));
        }

        [Test, Order(9)]
        public void StopApplication() 
        {
            //Process restaurantchainapp = Process.GetProcesses().Where(process => process.ProcessName.StartsWith("RestaurantChain")).Single();
            IEnumerable<Process> restaurantchainapps = Process.GetProcesses().Where(process => process.ProcessName.StartsWith("RestaurantChain"));
            foreach (Process restaurantchainapp in restaurantchainapps) 
            {
                restaurantchainapp.Kill();
            }

            Thread.Sleep(3000);

            int restaurantchainappExecutingInstances = Process.GetProcesses().Count(process => process.ProcessName.StartsWith("RestaurantChain"));
            Assert.That(restaurantchainappExecutingInstances, Is.EqualTo(0));
        }
    }
}