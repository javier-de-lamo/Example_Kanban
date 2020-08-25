using Microsoft.AspNetCore.Mvc;

// With this, Swagger can properly show the status codes returned from all the endpoints in the Controllers
[ assembly: ApiConventionType( typeof( DefaultApiConventions ) ) ]

namespace KanbanApi
{
    using System;
    using System.IO;
    using System.Reflection;
    using AppCore.Interfaces;
    using AppCore.Services;
    using AutoMapper;
    using Infraestructure.AutoMapper;
    using Infraestructure.Data.Context;
    using Infraestructure.Data.Repository;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        /// <summary>
        /// Contains configuration settings. By default, it contains the ones in appsettings.json
        /// </summary>
        public IConfiguration Configuration { get; }

        public Startup( IConfiguration configuration ) => this.Configuration = configuration;

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            // Entity Framework Config
            services.AddDbContext< KanbanDbContext >
                ( options => options.UseSqlServer( this.Configuration.GetConnectionString( "DefaultConnection" ) ) );

            // Auto Mapper Config
            services.AddAutoMapper( cfg => cfg.AddProfile< MappingProfile >( ), typeof( Startup ) );

            // Dependency injection
            services.AddScoped< ITaskService, TaskService >( );
            services.AddScoped< ITaskItemRepo, TaskItemRepo >( );

            // Configure Swagger
            services.AddSwaggerGen
            (
                config =>
                {
                    config.SwaggerDoc
                    (
                        "v1",
                        new OpenApiInfo
                        {
                            Version     = "V1",
                            Title       = "Simple Kanban Web API",
                            Description = "Simple ASP.NET API example, using a Kanban board",
                            License     = new OpenApiLicense { Name = "MIT", Url = new Uri( "http://bfy.tw/4nqh" ) }
                        }
                    );

                    var xmlFile = $"{Assembly.GetExecutingAssembly( ).GetName( ).Name}.xml";
                    var xmlPath = Path.Combine( AppContext.BaseDirectory, xmlFile );
                    config.IncludeXmlComments( xmlPath );
                }
            );

            services.AddControllers( );
        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            // Add Swagger to the pipeline
            app.UseSwagger( );
            // Add Swagger endpoint to the list of endpoints
            app.UseSwaggerUI( config => config.SwaggerEndpoint( "/swagger/v1/swagger.json", "API V1" ) );

            if( env.IsDevelopment( ) ) { app.UseDeveloperExceptionPage( ); }

            app.UseHttpsRedirection( );


            app.UseRouting( );

            app.UseAuthorization( );

            app.UseEndpoints( endpoints => endpoints.MapControllers( ) );
        }
    }
}