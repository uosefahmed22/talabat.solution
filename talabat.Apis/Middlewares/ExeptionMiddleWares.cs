using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using talabat.Apis.Errors;

namespace talabat.Apis.Middlewares
{
    public class ExeptionMiddleWares
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExeptionMiddleWares> logger;
        private readonly IHostEnvironment env;

        public ExeptionMiddleWares( RequestDelegate next , ILogger<ExeptionMiddleWares> logger , IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context); 
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = env.IsDevelopment() ? new ApiExeptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                                                   : new ApiExeptionResponse((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var jsonResponse = JsonSerializer.Serialize(response , options);
                await context.Response.WriteAsync(jsonResponse);

            }
        }
    }
}
