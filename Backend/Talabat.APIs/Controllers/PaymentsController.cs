using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using System.Reflection.Metadata;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services.Contract;
using Events = Stripe.Events;

namespace Talabat.APIs.Controllers
{

    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;
        private const string _whSecret = "whsec_6ed11ba6b8485827ea53b7c98d76db04828fe057979f93e3718ecd41fc2023cf";

        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }
        [Authorize]
        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("{basketId}")] // POST : /api/Payments/basketId
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket is null) return BadRequest(new ApiResponse(400, "An Error With Your Basket"));

            return Ok(basket);
        }


        [HttpPost("webhook")] // POST : /api/Payments/webhook
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
         
                var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], _whSecret);

                var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                Order order;
                switch (stripeEvent.Type)
                {
                    case "payment_intent.succeeded":
                        order = await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, true);
                        _logger.LogInformation("Payment is Succeeded", paymentIntent.Id);
                        break;
                    case "payment_intent.payment_failed":
                        order = await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, false);
                        _logger.LogInformation("Payment is Failed:(", paymentIntent.Id);
                        break;
                }
                //return new EmptyResult(); 
                return Ok();
        }
    }
}
