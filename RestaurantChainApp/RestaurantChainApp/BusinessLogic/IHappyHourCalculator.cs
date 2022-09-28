namespace RestaurantChainApp.BusinessLogic
{
    public interface IHappyHourCalculator
    {
        bool IsHappyHour(int currentHour, int happyHourBegin, int happyHourEnd);
    }
}
