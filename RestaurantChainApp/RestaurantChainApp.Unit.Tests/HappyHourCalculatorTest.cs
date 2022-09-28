using NUnit.Framework;
using RestaurantChainApp.BusinessLogic;
using RestaurantChainApp.Unit.Tests.Builders;
using RestaurantChainApp.Dtoes;

namespace RestaurantChainApp.Unit.Tests
{
    public class HappyHourCalculatorTest
    {
        private HappyHourCalculator happyHourCalculator;

        private HappyHourBuilder happyHourBuilder;

        [SetUp]
        public void Setup()
        {
            happyHourCalculator = new HappyHourCalculator();

            happyHourBuilder = new HappyHourBuilder();
        }

        [Test]
        public void HappyHourTrueTest()
        {
            HappyHour happyHour = happyHourBuilder.Build();

            bool isHappy = happyHourCalculator.IsHappyHour(currentHour: 14, happyHour.HappyHourBegin, happyHour.HappyHourEnd);

            Assert.IsTrue(isHappy);
        }

        [Test]
        public void HappyHourFalseTest()
        {
            HappyHour happyHour = happyHourBuilder.Build();

            bool isHappy = happyHourCalculator.IsHappyHour(currentHour: 16, happyHour.HappyHourBegin, happyHour.HappyHourEnd);

            Assert.IsFalse(isHappy);
        }
    }
}
