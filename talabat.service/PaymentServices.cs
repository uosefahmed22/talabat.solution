using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.FinancialConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core;
using talabat.core.Entites.Basket;
using talabat.core.Entites.Order_Aggregate;
using talabat.core.Entites.Product;
using talabat.core.Repositories;
using talabat.core.services;

namespace talabat.service
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepositries _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentServices(
            IConfiguration configuration,
            IBasketRepositries basketRepo,
            IUnitOfWork unitOfWork
            )
        {
            _configuration = configuration;
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatedPaymentAsync(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSetting:Secretkey"];

            var basket =await _basketRepo.GetBasketAsync(BasketId);
            if (basket is null) return null;
            var ShippingCost = 0M;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod =await _unitOfWork.repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                ShippingCost = deliveryMethod.Cost;
                basket.ShippingCost = deliveryMethod.Cost;
            }
            
            if(basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.repository<core.Entites.Product.Product>().GetByIdAsync(item.id);
                    if(item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }
                }
            }

            PaymentIntent paymentIntent;
            var services = new PaymentIntentService();


            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                //Create
                var Options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(O => (O.Price * 100) * O.Quantity )+ (long)ShippingCost,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string>(){"card"}

                };
                paymentIntent = await services.CreateAsync(Options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            //Update
            else
            {
                var Options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(O => (O.Price * 100) * O.Quantity) + (long)ShippingCost
                };
                await services.UpdateAsync(basket.PaymentIntentId , Options);
            }
            await _basketRepo.UbdateBasketAsync(basket);
            return basket;

        }
    }
}
