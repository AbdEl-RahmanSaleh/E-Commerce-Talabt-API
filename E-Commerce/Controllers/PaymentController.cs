using E_Commerce.HandleResponses;
using Infrastructure.BasketRepository;
using Microsoft.AspNetCore.Mvc;
using Services.Services.BasketService;
using Services.Services.BasketService.Dto;
using Services.Services.OrderService.Dto;
using Services.Services.PaymentService;
using Stripe;

namespace E_Commerce.Controllers
{
    public class PaymentController : BaseController
    {
        private const string WhSecret = "whsec_f4040c3ad039e3f6d3150fe61ee78ce669271a69a483c92c19b05d143af61013";
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private readonly IBasketService _basketService;

        public PaymentController(IPaymentService paymentService,
                                 ILogger<PaymentController> logger,
                                 IBasketService basketService)
        {
            _paymentService = paymentService;
            _logger = logger;
            _basketService = basketService;
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntentForExistingOrder(CustomerBasketDto basket)
        {
            var customerBasket = await _paymentService.CreateOrUpdatePaymentIntentForExistingOrder(basket);

            if (customerBasket == null)
               return BadRequest(new ApiResponse(400, "Something is wrong abot the basket"));
            return Ok(customerBasket);   
        }
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntentForNewOrder(string basketId)
        {
            var customerBasket = await _paymentService.CreateOrUpdatePaymentIntentForNewOrder(basketId);

            if (customerBasket == null)
               return BadRequest(new ApiResponse(400, "Something is wrong abot the basket"));
            return Ok(customerBasket);   
        }

        [HttpPost]
        public async Task<ActionResult> WebHook(string basketId)
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,Request.Headers["Stripe-Signature"], WhSecret);

                PaymentIntent paymentIntent;
                OrderResultDto order;
                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment failed : ",paymentIntent.Id);

                    order = await _paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);
                    _logger.LogInformation("Order Updated To Payment Failed : ",order.Id); 
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeed : ", paymentIntent.Id);
                    order = await _paymentService.UpdateOrderPaymentSucceeded(paymentIntent.Id, basketId);
                    _logger.LogInformation("Order Updated To Payment Succeed : ", order.Id);
                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

    }
}
