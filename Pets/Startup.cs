namespace Pets
{
    using System.Reflection;
    using Autofac;
    using Autofac.Extensions.ConfiguredModules;
    using Filters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        public IConfiguration Configuration { get; }



        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services
                .AddSwagger();


            ConfigureServices(services);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddScoped<TransactionFilter>()
                .AddControllersWithViews(mvcOptions =>
                {
                    mvcOptions.Filters.AddService<TransactionFilter>();
                })
                .AddNewtonsoftJson();
        }



        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterConfiguredModulesFromCurrentAssembly(Configuration);
        }



        public void ConfigureDevelopment(IApplicationBuilder applicationBuilder, Database database)
        {
            applicationBuilder
                .UseDeveloperExceptionPage()
                .UseSwagger();


            Configure(applicationBuilder, database);
        }

        public void Configure(IApplicationBuilder applicationBuilder, Database database)
        {
            database.InitAsync().Wait();


            applicationBuilder
                .UseStaticFiles()
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapFallbackToController("Index", "Home");
                });
        }
    }
}
