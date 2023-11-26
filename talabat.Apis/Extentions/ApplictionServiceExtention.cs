using Microsoft.AspNetCore.Mvc;
using talabat.Apis.Errors;
using talabat.Apis.Helpers;
using talabat.core;
using talabat.core.Repositories;
using talabat.core.services;
using talabat.Repository;
using talabat.service;

namespace talabat.Apis.Extentions
{
    public static class ApplictionServiceExtention
    {
        public static IServiceCollection AddAplictionService( this IServiceCollection service  )
        {
            //service.AddScoped(typeof(iGenericRepository<>), typeof(GenericRepository<>));
            service.AddAutoMapper(typeof(MappingProfilles));
            service.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var Errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                                       .SelectMany(P => P.Value.Errors)
                                                                       .Select(E => E.ErrorMessage)
                                                                       .ToArray();
                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    { Errors = Errors };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });

            service.AddScoped<IUnitOfWork , UnitOfWorke> ();
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<IPaymentServices, PaymentServices>();
            service.AddSingleton< IResponseCashServices , ResponseCashServices>();
            return service;
        }
    }
}
