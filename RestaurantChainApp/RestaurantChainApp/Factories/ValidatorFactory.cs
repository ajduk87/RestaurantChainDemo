using RestaurantChainApp.Validators;

namespace RestaurantChainApp.Factories
{
    public class ValidatorFactory : IValidatorFactory
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        public ValidatorFactory(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            this.databaseConnectionFactory = databaseConnectionFactory;
        }

        public OrderCreateValidator OrderCreateValidator() 
        {
            return new OrderCreateValidator();
        }
        public OrderUpdateValidator OrderUpdateValidator() 
        {
            return new OrderUpdateValidator(databaseConnectionFactory);
        }
    }
}
