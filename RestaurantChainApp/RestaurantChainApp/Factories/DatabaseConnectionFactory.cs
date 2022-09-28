using Npgsql;
using RestaurantChainApp;
using RestaurantChainApp.Factories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantChainApp.Factories
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