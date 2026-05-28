using Investry.Application.Common;
using Investry.Application.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Investry.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly StripeSettings _stripeSettings;

        private readonly IConfiguration _configuration;
        public PaymentService(IOptions<StripeSettings> stripeSettings, IConfiguration configuration)
        {
            _stripeSettings = stripeSettings.Value;
            _configuration = configuration;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }
        public async Task<CheckoutResponse> CreateCheckoutSessionAsync(decimal amount, string userId, Dictionary<string, string> metadata)
        {
            try
            {
                var baseUrl = _configuration["Frontend:BaseUrl"];
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = "usd",
                                UnitAmount = (long)(amount * 100), // هحولخا لاصغر وحده
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = "Wallet Deposit",
                                    Description = $"Deposit funds into your Investry account wallet.",
                                },
                            },
                            Quantity = 1,
                        },
                    },
                    Mode = "payment",
                    SuccessUrl = $"{baseUrl}/wallet/success",
                    CancelUrl = $"{baseUrl}/wallet/cancel",
                    Metadata = metadata
                };

                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                return new CheckoutResponse
                {
                    SessionId = session.Id,
                    CheckoutUrl = session.Url
                };
            }
            catch (StripeException e)
            {
                throw new Exception($"Stripe error: {e.Message}");
            }
        }

        public WebhookResult ParseWebhookEvent(string payload, string signature)
        {
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(payload, signature, _stripeSettings.WebhookSecret);

                var session = (Session)stripeEvent.Data.Object;

                return new WebhookResult(
                    EventType: stripeEvent.Type,
                    SessionId: session.Id,
                    Amount: session.AmountTotal!.Value / 100m,
                    Metadata: session.Metadata
                    );
            }
            catch (StripeException e)
            {
                throw new Exception($"Webhook error: {e.Message}");
            }
        }
    }
}
