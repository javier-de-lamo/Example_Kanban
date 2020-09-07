using Microsoft.AspNetCore.Mvc;

// With this, Swagger can properly show the status codes returned from all the endpoints in the Controllers
[assembly: ApiConventionType( typeof( DefaultApiConventions ) )]

namespace KanbanApi
{
    using System.Text.Json.Serialization;
    using AppCore.Interfaces;
    using AppCore.Services;
    using AutoMapper;
    using Infraestructure.AutoMapper;
    using Infraestructure.Data.Context;
    using Infraestructure.Data.Repository;
    using KanbanApi.Swagger;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        /// <summary>
        ///     Contains configuration settings. By default, it contains the ones in appsettings.json
        /// </summary>
        public IConfiguration Configuration { get; }

        public Startup( IConfiguration configuration ) => this.Configuration = configuration;

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            // Entity Framework Config
            services.AddDbContext< KanbanDbContext >( options => options.UseSqlServer( this.Configuration.GetConnectionString( "DefaultConnection" ) ) );

            // Auto Mapper Config
            services.AddAutoMapper( cfg => cfg.AddProfile< MappingProfile >( ), typeof( Startup ) );

            // Dependency injection
            services.AddScoped< IToDoService, ToDoService >( );
            services.AddScoped< ITaskItemRepo, TaskItemRepo >( );

            // Configure Swagger
            services.AddCustomSwagger( );

            // Swagger will show enums as string using this option
            services.AddControllers( )
               .AddJsonOptions( options => options.JsonSerializerOptions.Converters.Add( new JsonStringEnumConverter( ) ) );
        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            // Add Swagger to the pipeline
            app.UseCustomSwagger( );

            if( env.IsDevelopment( ) ) { app.UseDeveloperExceptionPage( ); }

            app.UseHttpsRedirection( );


            app.UseRouting( );

            app.UseAuthorization( );

            app.UseEndpoints( endpoints => endpoints.MapControllers( ) );
        }
    }
}