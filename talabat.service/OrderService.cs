using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core;
using talabat.core.Entites.Order_Aggregate;
using talabat.core.Entites.Product;
using talabat.core.Repositories;
using talabat.core.services;
using talabat.core.Specifications;

namespace talabat.service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepositries _basketRepositries;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentServices _paymentServices;

        public OrderService(IBasketRepositries basketRepositries,IUnitOfWork unitOfWork,IPaymentServices paymentServices)
        {
            _basketRepositries = basketRepositries;
            _unitOfWork = unitOfWork;
            _paymentServices = paymentServices;
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, core.Entites.Order_Aggregate.Address ShippingAddress)
        {
            // 1. Get basket From Basket Repo
            var Basket = await _basketRepositries.GetBasketAsync(BasketId);

            // 2. Get Selected Items at Basket From Product Repo
            var OrderItems = new List<OrderItem>();

            if (Basket?.Items?.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product = await _unitOfWork.repository<core.Entites.Product.Product>().GetByIdAsync(item.id);
                    var ProductItemOrdered = new PrductItemOrder(Product.id , Product.Name , Product.PictureUrl);
                    var OrderdItem = new OrderItem(ProductItemOrdered , item.Quantity , Product.Price);
                    OrderItems.Add(OrderdItem);

                }
            }
            
            // 3. Calculate SubTotal
            var SubTotal = OrderItems.Sum(x => x.Price * x.Quantity);
            
            // 4. Get Delivery Method From Delivery Method Repo
            
            var deleverymethod = await _unitOfWork.repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);

            // 5. Create Order 
            var spec = new OrderWithPaymentSpec(Basket.PaymentIntentId);
            var ExsistingOrder= await _unitOfWork.repository<Order>().GetEntityWithSpecAsync(spec);
            if (ExsistingOrder != null)
            {
                _unitOfWork.repository<Order>().Delete(ExsistingOrder);
                await _paymentServices.CreateOrUpdatedPaymentAsync(BasketId);
            }
            var order = new Order(BuyerEmail , ShippingAddress , deleverymethod ,
                OrderItems , SubTotal, Basket.PaymentIntentId);

            await _unitOfWork.repository<Order>().AddAsync(order);

            // 6. Save To Database [TODO]

            var result =  await _unitOfWork.Complete();
            if (result <= 0) return null;
            return order;

        }

        public async Task<Order> CreateOrderByIdForUserAsync(string BuyerEmail, int OrderId)
        {
            var Spec = new OrderSpacifications(OrderId, BuyerEmail);
           var Orders =await _unitOfWork.repository<Order>().GetEntityWithSpecAsync(Spec);
            return Orders;
        }

        public async Task<IReadOnlyList<Order>> CreateOrdersForUserAsync(string BuyerEmail)
        {
            var spec = new OrderSpacifications(BuyerEmail);
            var Orders = await _unitOfWork.repository<Order>().GetAllWithSpecAsync(spec);
            return Orders;
        }

        public async Task<IReadOnlyList< DeliveryMethod>> GetDeliverymethods()
        {
            var deliverymethods =await _unitOfWork.repository<DeliveryMethod>().GetAllAsync();
            return deliverymethods;
        }
    }
}
