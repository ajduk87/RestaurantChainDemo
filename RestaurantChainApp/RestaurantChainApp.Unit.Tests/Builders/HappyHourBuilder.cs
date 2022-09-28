using RestaurantChainApp.Dtoes;

namespace RestaurantChainApp.Unit.Tests.Builders
{
    public class HappyHourBuilder
    {
        public HappyHour Build() 
        {
            return new HappyHour 
            {
                HappyHourBegin = 13,
                HappyHourEnd = 15
            };
        }
    }
}
