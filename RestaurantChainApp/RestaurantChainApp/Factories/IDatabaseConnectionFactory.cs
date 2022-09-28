using Npgsql;

namespace RestaurantChainApp.Factories
{
    public interface IDatabaseConnectionFactory
    {
        NpgsqlConnection Create(string connectionStringParam = null);
    }
}