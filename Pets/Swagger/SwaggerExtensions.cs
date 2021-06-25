namespace Pets.Swagger
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            if (services == null) 
                throw new ArgumentNullException(nameof(services));

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

                    options.CustomSchemaIds(type => type.FullName);
                });

            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder applicationBuilder)
        {
            if (applicationBuilder == null) 
                throw new ArgumentNullException(nameof(applicationBuilder));

            applicationBuilder
                .UseSwagger(options =>
                {
                })
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pets API");
                });

            return applicationBuilder;
        }
    }
}
