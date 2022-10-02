using Npgsql;

namespace RestaurantChainAppQueries.Factories
{
    public interface IDatabaseConnectionFactory
    {
        NpgsqlConnection Create(string connectionStringParam = null);
    }
}
