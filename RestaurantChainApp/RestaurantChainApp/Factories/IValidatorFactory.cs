using RestaurantChainApp.Validators;

namespace RestaurantChainApp.Factories
{
    public interface IValidatorFactory
    {
        OrderCreateValidator OrderCreateValidator();
        OrderUpdateValidator OrderUpdateValidator();
    }
}
