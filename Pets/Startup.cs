namespace Pets
{
    using System.Reflection;
    using Autofac;
    using Autofac.Extensions.ConfiguredModules;
    using Filters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Persistence;

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
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(
                        name: "v1",
                        info: new OpenApiInfo
                        {
                            Title = "Pets API",
                            Version = "v1"
                        });

                    options.CustomSchemaIds(x => x.FullName);
                });


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



        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterConfiguredModulesFromCurrentAssembly(Configuration);
        }



        public void ConfigureDevelopment(IApplicationBuilder app, Database database)
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pets API");
            });


            Configure(app, database);
        }

        public void Configure(IApplicationBuilder app, Database database)
        {
            database.InitAsync().Wait();

            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Home");
            });
        }
    }
}
