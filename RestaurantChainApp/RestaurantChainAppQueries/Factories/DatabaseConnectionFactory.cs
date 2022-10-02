using Npgsql;

namespace RestaurantChainAppQueries.Factories
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly EnvironmentSettings envSettings;

        public DatabaseConnectionFactory(IEnvironmentSettingsFactory environmentSettingsFactory)
        {
            envSettings = environmentSettingsFactory.GetEnvironmentSettings();
        }


        public NpgsqlConnection Create(string connectionStringParam = null)
        {
            return new NpgsqlConnection(envSettings.ConnectionString);
        }
    }
}
