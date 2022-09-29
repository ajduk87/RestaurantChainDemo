using RestaurantChainApp.Validators;

namespace RestaurantChainApp.Factories
{
    public class ValidatorFactory : IValidatorFactory
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;
        private readonly IRepositoryFactory repositoryFactory;

        public ValidatorFactory(IDatabaseConnectionFactory databaseConnectionFactory, IRepositoryFactory repositoryFactory)
        {
            this.databaseConnectionFactory = databaseConnectionFactory;
            this.repositoryFactory = repositoryFactory;
        }

        public OrderCreateValidator OrderCreateValidator() 
        {
            return new OrderCreateValidator();
        }
        public OrderUpdateValidator OrderUpdateValidator() 
        {
            return new OrderUpdateValidator(databaseConnectionFactory, repositoryFactory);
        }
    }
}
