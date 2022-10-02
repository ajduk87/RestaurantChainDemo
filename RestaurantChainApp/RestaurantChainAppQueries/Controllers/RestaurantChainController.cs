using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;
using RestaurantChainAppQueries.Dtoes;
using RestaurantChainAppQueries.Factories;
using RestaurantChainAppQueries.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantChainAppQueries.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantChainController : ControllerBase
    {
       
        private readonly ILogger<RestaurantChainController> _logger;

        private readonly IDatabaseConnectionFactory databaseConnectionFactory;
        private readonly DishesRepository dishesRepository;
        private readonly OrderDtoRepository orderDtoRepository;
        private readonly EnvironmentSettings environmentSettings;

        public RestaurantChainController(IRepositoryFactory repositoryFactory,
                                         ILogger<RestaurantChainController> logger,
                                         IDatabaseConnectionFactory databaseConnectionFactory,
                                         IEnvironmentSettingsFactory environmentSettingsFactory)
        {
            this.databaseConnectionFactory = databaseConnectionFactory;

            dishesRepository = repositoryFactory.CreateDishesRepository();
            orderDtoRepository = repositoryFactory.CreateOrderDtoRepository();

            environmentSettings = environmentSettingsFactory.GetEnvironmentSettings();

            _logger = logger;
            this.databaseConnectionFactory = databaseConnectionFactory;
        }

        [Route("GetMenu")]
        [HttpGet]
        public List<Dish> GetMenuItems()
        {
            using (NpgsqlConnection connection = databaseConnectionFactory.Create())
            {
                try
                {
                    connection.Open();
                    return dishesRepository.SelectDishes(connection);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new List<Dish>();
                }                
                
            }
        }


        [Route("GetSingleDishes")]
        [HttpGet]
        public List<Dish> GetSingleDishes()
        {
            using (NpgsqlConnection connection = databaseConnectionFactory.Create())
            {
                try
                {
                    connection.Open();
                    return dishesRepository.SelectSingleDishes(connection);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new List<Dish>();
                }

            }
        }

        [Route("GetMeals")]
        [HttpGet]
        public List<Meal> GetMeals()
        {
            using (NpgsqlConnection connection = databaseConnectionFactory.Create())
            {
                try
                {
                    connection.Open();

                    List<Meal> meals = dishesRepository.SelectMeals(connection);

                    foreach (var meal in meals) 
                    {
                        meal.Dishes = dishesRepository.SelectDishesByMealId(connection, meal.Id);
                    }

                    return meals;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new List<Meal>();
                }

            }
        }

        [Route("HappyHour")]
        [HttpGet]
        public HappyHour HappyHour()
        {
            return new HappyHour 
            {
                HappyHourBegin = environmentSettings.HappyHourBegin,
                HappyHourEnd = environmentSettings.HappyHourEnd
            };
        }

        [Route("GetOrder")]
        [HttpGet]
        public OrderDto GetOrder(int orderid)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Create())
            {
                connection.Open();

                try
                {

                    OrderDto orderDto = orderDtoRepository.SelectOrder(connection, orderid);
                    orderDto.orderItems = orderDtoRepository.SelectOrderItems(connection, orderid);


                    return orderDto;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return new OrderDto();
                }

            }
        }

    }
}
