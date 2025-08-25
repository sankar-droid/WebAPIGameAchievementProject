namespace WebAPIProject.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .WithMethods("GET", "POST", "PUT", "DELETE")
                           .AllowAnyHeader();
                });

            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = true;
                options.AutomaticAuthentication = false;
            });
        }


    }
}
