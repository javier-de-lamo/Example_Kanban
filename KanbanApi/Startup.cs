namespace KanbanApi
{
    using AutoMapper;
    using Infraestructure.AutoMapper;
    using Infraestructure.Data.Context;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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

            services.AddControllers( );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if( env.IsDevelopment( ) ) { app.UseDeveloperExceptionPage( ); }

            app.UseHttpsRedirection( );

            app.UseRouting( );

            //app.UseAuthorization( );

            app.UseEndpoints( endpoints => endpoints.MapControllers( ) );
        }
    }
}