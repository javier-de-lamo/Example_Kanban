namespace KanbanApi.MiddleWare
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    /// <summary>
    ///     Extends IServiceCollection to configure Swagger and mantain a clean Startur.cs class
    /// </summary>
    public static class ConfigureSwagger
    {
        public static IServiceCollection AddCustomSwagger( this IServiceCollection services )
        {
            services.AddSwaggerGen
            (
                cfg =>
                {
                    cfg.SwaggerDoc
                    (
                        "v1",
                        new OpenApiInfo
                        {
                            Version     = "V1",
                            Title       = "Simple Kanban Web API",
                            Description = "Simple ASP.NET API example, using a Kanban board",
                            License     = new OpenApiLicense { Name = "MIT", Url = new Uri( "http://bfy.tw/4nqh" ) },
                        }
                    );

                    var xmlFile = $"{Assembly.GetExecutingAssembly( ).GetName( ).Name}.xml";
                    var xmlPath = Path.Combine( AppContext.BaseDirectory, xmlFile );
                    cfg.IncludeXmlComments( xmlPath );
                }
            );
            return services;
        }

        public static IApplicationBuilder UseCustomSwagger( this IApplicationBuilder app )
        {
            app.UseSwagger( )
               .UseSwaggerUI
                (
                    options =>
                    {
                        options.SwaggerEndpoint( "/swagger/v1/swagger.json", "API V1" );
                        options.DocumentTitle = "Kanban API";
                    }
                );
            return app;
        }
    }
}