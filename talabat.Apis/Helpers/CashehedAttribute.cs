using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text;
using talabat.core.services;

namespace talabat.Apis.Helpers
{
    public class CashehedAttribute:Attribute,IAsyncActionFilter
    {
        private readonly int _timeAlive;

        public CashehedAttribute(int TimeAlive) 
        {
            _timeAlive = TimeAlive;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CashService = context.HttpContext.RequestServices.GetRequiredService<IResponseCashServices>();
            var CashKey = GeneratecashKeyFromRequest(context.HttpContext.Request);
            var CashResponse = await CashService.GetCashedResponseAsync(CashKey);

            if (!string.IsNullOrEmpty(CashResponse))
            {
                var ContentResult = new ContentResult()
                {
                    Content = CashResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = ContentResult;
                return;
            }
            var ExcutedEndPointContext = await next();
            if (ExcutedEndPointContext.Result is ObjectResult objResult)
            {
                await CashService.CashResponseAsync(CashKey, objResult,TimeSpan.FromSeconds(_timeAlive));
            }

        }
        private string GeneratecashKeyFromRequest(HttpRequest Request)
        {
            var Keybuilder = new StringBuilder();
            Keybuilder.Append(Request.Path);
            foreach (var (key,Value) in Request.Query.OrderBy(X=>X.Key))
            {
                Keybuilder.Append($"|{key}-{Value}");
            }
            return Keybuilder.ToString();
        }
    }
}
