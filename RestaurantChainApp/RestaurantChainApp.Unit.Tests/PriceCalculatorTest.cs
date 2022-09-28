using NUnit.Framework;
using RestaurantChainApp.BusinessLogic;
using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Unit.Tests.Builders;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantChainApp.Unit.Tests
{
    public class PriceCalculatorTest
    {
        private PriceCalculator priceCalculator;

        private DishesBuilder dishesBuilder;
        private MealsBuilder mealsBuilder;

        [SetUp]
        public void Setup()
        {
            priceCalculator = new PriceCalculator();

            dishesBuilder = new DishesBuilder();
            mealsBuilder = new MealsBuilder();
        }

        [Test]
        public void CalculateDishesPriceWithoutHappyHourTest()
        {
            List<Dish> dishes = dishesBuilder.Build();

            dishes = priceCalculator.CalculateForDishes(dishes, isHappyHour: false);

            Assert.That(dishes.First().Price, Is.EqualTo(3.54));
        }

        [Test]
        public void CalculateDishesPriceWithHappyHourTest()
        {
            List<Dish> dishes = dishesBuilder.Build();

            dishes = priceCalculator.CalculateForDishes(dishes, isHappyHour: true);

            Assert.That(dishes.First().Price, Is.EqualTo(2.83));
        }

        [Test]
        public void CalculateMealsPriceWithoutHappyHourTest()
        {
            List<Meal> meals = mealsBuilder.Build();

            meals = priceCalculator.CalculateForMeals(meals, isHappyHour: false);

            Assert.That(meals.First().Price, Is.EqualTo(13.1));
        }

        [Test]
        public void CalculateMealsPriceWithHappyHourTest()
        {
            List<Meal> meals = mealsBuilder.Build();

            meals = priceCalculator.CalculateForMeals(meals, isHappyHour: true);

            Assert.That(meals.First().Price, Is.EqualTo(10.48));
        }
    }
}