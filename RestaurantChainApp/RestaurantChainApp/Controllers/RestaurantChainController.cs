using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantChainApp.BusinessLogic;
using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Entities;
using RestaurantChainApp.Factories;
using RestaurantChainApp.Mappings;
using RestaurantChainApp.Models.Order;
using RestaurantChainApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestaurantChainApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantChainController : ControllerBase
    {      

        private readonly ILogger<RestaurantChainController> logger;
        private IRestaurantChainService restaurantChainService;
        private IValidatorFactory validatorFactory;
        private readonly IMapper mapper;

        public RestaurantChainController(IRestaurantChainService restaurantChainService,
                                         IValidatorFactory validatorFactory,
                                         ILogger<RestaurantChainController> logger)
        {
            this.restaurantChainService = restaurantChainService;
            this.validatorFactory = validatorFactory;
            this.logger = logger;

            this.mapper = GenerateMapper();
        }

        private IMapper GenerateMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ModelsToDtoes>();
            });

            return mapperConfiguration.CreateMapper();
        }

        //[Route("GetMenu")]
        //[HttpGet]        
        //public List<Dish> GetMenuItems()
        //{
        //    return this.restaurantChainService.GetMenu();
        //}

        //[Route("GetSingleDishes")]
        //[HttpGet]
        //public List<Dish> GetSingleDishes()
        //{
        //    return this.restaurantChainService.GetSingleDishes();
        //}

        //[Route("GetMeals")]
        //[HttpGet]
        //public List<Meal> GetMeals()
        //{
        //    return this.restaurantChainService.GetMeals();
        //}

        //[Route("HappyHour")]
        //[HttpGet]
        //public HappyHour HappyHour()
        //{
        //    return this.restaurantChainService.HappyHour();
        //}

        [Route("GetOrder")]
        [HttpGet]
        public OrderDto GetOrder(int orderid)
        {
            return this.restaurantChainService.GetOrder(orderid);
        }

        [Route("CreateOrder")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(CreateOrderModel createOrderModel)
        {
            ValidationResult validationResult = validatorFactory.OrderCreateValidator().Validate(createOrderModel);
            if (!validationResult.IsValid) 
            {
                List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                logger.LogError(string.Join(",", errors));
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            OrderDto orderDto = this.mapper.Map<OrderDto>(createOrderModel);
            this.restaurantChainService.CreateOrder(orderDto);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Route("ModifyOrder")]
        [HttpPut]
        public HttpResponseMessage ModifyOrder(UpdateOrderModel updateOrderModel)
        {
            ValidationResult validationResult = validatorFactory.OrderUpdateValidator().Validate(updateOrderModel);
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                logger.LogError(string.Join(",", errors));
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            OrderDto orderDto = this.mapper.Map<OrderDto>(updateOrderModel);
            this.restaurantChainService.ModifyOrder(orderDto);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        
    }
}
