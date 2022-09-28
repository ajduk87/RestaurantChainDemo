using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantChainApp.Factories
{
    public class EnvironmentSettingsFactory : IEnvironmentSettingsFactory
    {
        private const string DEFAULT_ENVIRONMENT = "DefaultEnv";
        private IConfiguration _configuration;
        private IWebHostEnvironment _env;

        public EnvironmentSettingsFactory(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public EnvironmentSettings GetEnvironmentSettings()
        {
            return GetEnvironmentSettings(_configuration, _env);
        }

        internal static EnvironmentSettings GetEnvironmentSettings(IConfiguration configuration, IWebHostEnvironment env)
        {
            return GetEnvironmentSettings(configuration, env.EnvironmentName);
        }

        internal static EnvironmentSettings GetEnvironmentSettings(IConfiguration configuration, string environmentName = null)
        {
            var environment = configuration.GetChildren().Any(item => item.Key == environmentName) ? environmentName : DEFAULT_ENVIRONMENT;
            var environmentSettings = new EnvironmentSettings();
            var environmentConfigurationSection = configuration.GetSection(environment);
            environmentConfigurationSection.Bind(environmentSettings);
            return environmentSettings;
        }
    }
}
