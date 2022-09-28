namespace RestaurantChainApp.BusinessLogic
{
    public class HappyHourCalculator : IHappyHourCalculator
    {
        public bool IsHappyHour(int currentHour, int happyHourBegin, int happyHourEnd)
        {
            return currentHour >= happyHourBegin && currentHour <= happyHourEnd;
        }
    }
}
