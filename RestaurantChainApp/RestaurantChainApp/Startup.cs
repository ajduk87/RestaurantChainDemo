using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RestaurantChainApp.BusinessLogic;
using RestaurantChainApp.Factories;
using RestaurantChainApp.Services;

namespace RestaurantChainApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }));



            services.AddTransient<IEnvironmentSettingsFactory, EnvironmentSettingsFactory>();
            services.AddTransient<IRepositoryFactory, RepositoryFactory>();
            services.AddTransient<IValidatorFactory, ValidatorFactory>();
            services.AddTransient<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
            services.AddTransient<IRestaurantChainService, RestaurantChainService>();
            services.AddTransient<IPriceCalculator, PriceCalculator>();
            services.AddTransient<IHappyHourCalculator, HappyHourCalculator>();


            services.AddLocalization();
            //Configure Swagger
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            Sql.Load();
        }
    }
}
