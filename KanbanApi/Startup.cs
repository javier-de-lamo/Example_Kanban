namespace KanbanApi
{
    using AppCore.Interfaces;
    using AppCore.Services;
    using AutoMapper;
    using Infraestructure.AutoMapper;
    using Infraestructure.Data.Context;
    using Infraestructure.Data.Repository;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup( IConfiguration configuration ) => this.Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
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
                ( config => config.SwaggerDoc( "v1", new OpenApiInfo { Version = "V1", Title = "Kanban Web Api", } ) );

            services.AddControllers( );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            app.UseSwagger( );

            app.UseSwaggerUI( config => config.SwaggerEndpoint( "/swagger/v1/swagger.json", "API V1" ) );

            if( env.IsDevelopment( ) ) { app.UseDeveloperExceptionPage( ); }

            app.UseHttpsRedirection( );

            app.UseRouting( );

            //app.UseAuthorization( );

            app.UseEndpoints( endpoints => endpoints.MapControllers( ) );
        }
    }
}