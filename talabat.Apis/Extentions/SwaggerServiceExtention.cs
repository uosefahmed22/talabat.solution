namespace talabat.Apis.Extentions
{
    public static class SwaggerServiceExtention
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection service )
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();
            return service;
        }


        public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
