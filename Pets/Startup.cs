namespace Pets
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Providers;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        public IConfiguration Configuration { get; }



        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson();
            
            services.AddSwaggerGen(options =>
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

            string connectionString = Configuration.GetConnectionString("Pets");
            DatabaseProvider.Init(connectionString);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pets API");
                });
            }

            // app.UseHttpsRedirection();

            app.UseStaticFiles();
            
            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapFallbackToController("Index", "Home");
            });
        }
    }
}
