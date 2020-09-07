namespace KanbanApi.MiddleWare
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class ConfigureCors
    {
        public static IServiceCollection AddCustomCors( this IServiceCollection services )
        {
            services.AddCors
            (
                options => options.AddPolicy( "CorsPolicy", builder => builder.AllowAnyOrigin( ).AllowAnyMethod( ).AllowAnyHeader( ) )
            );
            return services;
        }

        public static IApplicationBuilder UseCustomCors( this IApplicationBuilder app )
        {
            app.UseCors( "CorsPolicy" );
            return app;
        }
    }
}