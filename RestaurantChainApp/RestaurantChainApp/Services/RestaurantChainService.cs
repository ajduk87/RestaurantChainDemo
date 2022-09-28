using RestaurantChainApp.BusinessLogic;
using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Entities;
using RestaurantChainApp.Repositories;
using RestaurantChainApp.Factories;
using Npgsql;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantChainApp.Mappings;

namespace RestaurantChainApp.Services
{
    public class RestaurantChainService : IRestaurantChainService
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        private IPriceCalculator priceCalculator { get; set; }
        private IHappyHourCalculator happyHourCalculator { get; set; }

        private readonly MenuItemsRepository menuItemsRepository;
        private readonly OrdersRepository ordersRepository;
        private readonly OrderItemsRepository orderItemsRepository;

        private readonly IMapper mapper;
        private readonly EnvironmentSettings envSettings;

        public RestaurantChainService(IPriceCalculator priceCalculator, 
                                      IRepositoryFactory repositoryFactory,
                                      IDatabaseConnectionFactory databaseConnectionFactory,
                                      IHappyHourCalculator happyHourCalculator,
                                      IEnvironmentSettingsFactory environmentSettingsFactory)
        {
            this.priceCalculator = priceCalculator;
            this.happyHourCalculator = happyHourCalculator;
            this.databaseConnectionFactory = databaseConnectionFactory;

            menuItemsRepository = repositoryFactory.CreateMenuItemsRepository();
            ordersRepository = repositoryFactory.CreateOrdersRepository();
            orderItemsRepository = repositoryFactory.CreateOrderItemsRepository();

            this.mapper = GenerateMapper();
            this.happyHourCalculator = happyHourCalculator;
            envSettings = environmentSettingsFactory.GetEnvironmentSettings();
        }

        private IMapper GenerateMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntitiesToDtoesProfile>();
                cfg.AddProfile<DtoToEntitesProfile>();
            });

            return mapperConfiguration.CreateMapper();
        }

        public List<Dish> GetMenu() 
        {
            using (NpgsqlConnection connection = databaseConnectionFactory.Create())
            {
             connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            List<MenuItem> menuItems = menuItemsRepository.SelectMenuItems(connection, transaction);

                            List<Dish> dishes = this.mapper.Map<List<Dish>>(menuItems.Where(menuitem => !menuitem.IsMeal));
                            List<Meal> meals = this.mapper.Map<List<Meal>>(menuItems.Where(menuitem => menuitem.IsMeal));

                            foreach (var meal in meals)
                            {
                                List<MenuItem> menuItemsForMeal = menuItemsRepository.SelectMenuItemsByMealId(connection, meal.Id, transaction);

                                meal.Dishes = this.mapper.Map<List<Dish>>(menuItemsForMeal);
                            }

                            bool isHappyHour = happyHourCalculator.IsHappyHour(currentHour: DateTime.Now.Hour, envSettings.HappyHourBegin, envSettings.HappyHourEnd);

                            dishes = priceCalculator.CalculateForDishes(dishes, isHappyHour);
                            meals = priceCalculator.CalculateForMeals(meals, isHappyHour);

                            dishes.AddRange(meals);

                            menuItems = this.mapper.Map<List<MenuItem>>(dishes);

                            foreach (var menuItem in menuItems)
                            {
                                menuItemsRepository.Update(connection, menuItem, transaction);
                            }
                            transaction.Commit();

                            return dishes;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return new List<Dish>();
                        }
                    }
              
            }

          
        }

        public List<Dish> GetSingleDishes() 
        {
            using (NpgsqlConnection connection = databaseConnectionFactory.Create())
            {
              connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            List<MenuItem> menuItems = menuItemsRepository.SelectMenuItemsByIsMeal(connection, ismeal: false, transaction);

                            List<Dish> dishes = this.mapper.Map<List<Dish>>(menuItems);
                            bool isHappyHour = happyHourCalculator.IsHappyHour(currentHour: DateTime.Now.Hour, envSettings.HappyHourBegin, envSettings.HappyHourEnd);

                            dishes = priceCalculator.CalculateForDishes(dishes, isHappyHour);

                            menuItems = this.mapper.Map<List<MenuItem>>(dishes);

                            foreach (var menuItem in menuItems)
                            {
                                menuItemsRepository.Update(connection, menuItem, transaction);
                            }
                            transaction.Commit();

                        return dishes;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return new List<Dish>();
                        }

                    }
            }
                
        }
        public List<Meal> GetMeals() 
        {
            using (NpgsqlConnection connection = databaseConnectionFactory.Create())
            {
               connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            List<MenuItem> menuItems = menuItemsRepository.SelectMenuItemsByIsMeal(connection, ismeal: true, transaction);

                            List<Meal> meals = this.mapper.Map<List<Meal>>(menuItems.Where(menuitem => menuitem.IsMeal));

                            foreach (var meal in meals)
                            {
                                List<MenuItem> menuItemsForMeal = menuItemsRepository.SelectMenuItemsByMealId(connection, meal.Id, transaction);

                                meal.Dishes = this.mapper.Map<List<Dish>>(menuItemsForMeal);

                                //menuItemsForMeal.ForEach(item => 
                                //{
                                //    Dish dish = this.mapper.Map<Dish>(item);
                                //    meal.AddDish(dish);
                                //});                           

                            }

                            bool isHappyHour = happyHourCalculator.IsHappyHour(currentHour: DateTime.Now.Hour, envSettings.HappyHourBegin, envSettings.HappyHourEnd);

                            meals = priceCalculator.CalculateForMeals(meals, isHappyHour);

                                menuItems = this.mapper.Map<List<MenuItem>>(meals);

                                foreach (var menuItem in menuItems)
                                {
                                    menuItemsRepository.Update(connection, menuItem, transaction);
                                }
                                transaction.Commit();

                            return meals;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return new List<Meal>();
                        }
                    }
                

            }
        }

        public OrderDto GetOrder(int orderid)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Create())
            {
                connection.Open();
                
                    try
                    {
                        OrderDto orderDto = new OrderDto();

                        Order order = ordersRepository.SelectOrder(connection, orderid);
                        List<OrderItem> orderItems = orderItemsRepository.SelectOrderItems(connection, orderid);

                        orderDto = this.mapper.Map<OrderDto>(order);
                        orderDto.orderItems = this.mapper.Map<List<OrderItemDto>>(orderItems);

                        return orderDto;
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        return new OrderDto();
                    }
                
            }
        }

        public void CreateOrder(OrderDto orderDto) 
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Create())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        Order order = this.mapper.Map<Order>(orderDto);
                        List<OrderItem> orderItems = this.mapper.Map<List<OrderItem>>(orderDto.orderItems);

                        long orderid = ordersRepository.Insert(connection, order, transaction);

                        foreach (var orderItem in orderItems)
                        {
                            orderItem.OrderId = orderid;
                            orderItemsRepository.Insert(connection, orderItem, transaction);
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.Write(ex.Message);
                    }
                }
            }
        }

        public void ModifyOrder(OrderDto orderDto) 
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Create())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        Order order = this.mapper.Map<Order>(orderDto);
                        List<OrderItem> orderItems = this.mapper.Map<List<OrderItem>>(orderDto.orderItems);

                        ordersRepository.Update(connection, order, transaction);

                        foreach (var orderItem in orderItems)
                        {
                            orderItemsRepository.Update(connection, orderItem, transaction);
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.Write(ex.Message);
                    }
                }
            }
        }

        public HappyHour HappyHour() 
        {
            return new HappyHour 
            {
                HappyHourBegin = envSettings.HappyHourBegin,
                HappyHourEnd = envSettings.HappyHourEnd
            };
        }

    }
}
