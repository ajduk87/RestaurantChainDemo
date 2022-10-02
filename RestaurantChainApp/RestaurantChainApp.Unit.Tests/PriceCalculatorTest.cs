using NUnit.Framework;
using RestaurantChainApp.BusinessLogic;
using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Unit.Tests.Builders;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace RestaurantChainApp.Unit.Tests
{
    public class PriceCalculatorTest
    {
        private PriceCalculator priceCalculator;
        private Mock<IHappyHourCalculator> mockIHappyHourCalculator;
        private HappyHourBuilder happyHourBuilder;

        private DishesBuilder dishesBuilder;
        private MealsBuilder mealsBuilder;

        [SetUp]
        public void Setup()
        {
            mockIHappyHourCalculator = new Mock<IHappyHourCalculator>();
            priceCalculator = new PriceCalculator(mockIHappyHourCalculator.Object);

            dishesBuilder = new DishesBuilder();
            mealsBuilder = new MealsBuilder();

            happyHourBuilder = new HappyHourBuilder();
        }

        [Test]
        public void CalculateDishesPriceWithoutHappyHourTest()
        {
            HappyHour happyHour = happyHourBuilder.Build();
            mockIHappyHourCalculator.Setup(m => m.IsHappyHour(16, happyHour.HappyHourBegin, happyHour.HappyHourEnd)).Returns(false);

            
            List<Dish> dishes = dishesBuilder.Build();

            dishes = priceCalculator.CalculateForDishes(dishes, hourForCalculation:16, happyHour.HappyHourBegin, happyHour.HappyHourEnd);

            Assert.That(dishes.First().Price, Is.EqualTo(3.54));
        }

        [Test]
        public void CalculateDishesPriceWithHappyHourTest()
        {
            HappyHour happyHour = happyHourBuilder.Build();
            mockIHappyHourCalculator.Setup(m => m.IsHappyHour(14, happyHour.HappyHourBegin, happyHour.HappyHourEnd)).Returns(true);

            List<Dish> dishes = dishesBuilder.Build();

            dishes = priceCalculator.CalculateForDishes(dishes, hourForCalculation: 14, happyHour.HappyHourBegin, happyHour.HappyHourEnd);

            Assert.That(dishes.First().Price, Is.EqualTo(2.83));
        }

        [Test]
        public void CalculateMealsPriceWithoutHappyHourTest()
        {
            HappyHour happyHour = happyHourBuilder.Build();
            mockIHappyHourCalculator.Setup(m => m.IsHappyHour(16, happyHour.HappyHourBegin, happyHour.HappyHourEnd)).Returns(false);

            List<Meal> meals = mealsBuilder.Build();

            meals = priceCalculator.CalculateForMeals(meals, hourForCalculation: 16, happyHour.HappyHourBegin, happyHour.HappyHourEnd);

            Assert.That(meals.First().Price, Is.EqualTo(13.1));
        }

        [Test]
        public void CalculateMealsPriceWithHappyHourTest()
        {
            HappyHour happyHour = happyHourBuilder.Build();
            mockIHappyHourCalculator.Setup(m => m.IsHappyHour(14, happyHour.HappyHourBegin, happyHour.HappyHourEnd)).Returns(true);

            List<Meal> meals = mealsBuilder.Build();

            meals = priceCalculator.CalculateForMeals(meals, hourForCalculation: 14, happyHour.HappyHourBegin, happyHour.HappyHourEnd);

            Assert.That(meals.First().Price, Is.EqualTo(10.48));
        }
    }
}