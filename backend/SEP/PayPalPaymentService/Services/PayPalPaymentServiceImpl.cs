using PayPal.Api;
using PayPalPaymentService.Dto;
using PayPalPaymentService.Interfaces;
using System;

namespace PayPalPaymentService.Services
{
    public class PayPalPaymentServiceImpl : IPayPalPaymentService
    {
        public async Task<string> MakePayment(ServiceOfferDto offerDto)
        {
            var _config = ConfigManager.Instance.GetProperties();
            var _accessToken = new OAuthTokenCredential(_config).GetAccessToken();

            var apiContext = new APIContext
            {
                AccessToken = _accessToken,
                Config = ConfigManager.Instance.GetProperties()
            };

            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        amount = new Amount
                        {
                            currency = "USD",
                            total = Math.Round(offerDto.TotalPrice, 2).ToString()
                        },
                        description = $"{offerDto.Id}"
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    cancel_url = $"https://localhost:7140/api/PayPalPayment/payment/cancel/{offerDto.Id}",
                    return_url = $"https://localhost:7140/api/PayPalPayment/payment/success/{offerDto.Id}",
                }
            };

            var createdPayment = payment.Create(apiContext);

            var approval = createdPayment.links.Find(x => x.rel == "approval_url")
                ?? throw new Exception("No approval link");

            return approval.href;
        }

        public async Task CancelPayPalPayment(int orderId)
        {
            
        }

        public async Task<Payment> SuccessPayPalPayment(string paymentId, string payerId, int orderId)
        {
            var _config = ConfigManager.Instance.GetProperties();
            var _accessToken = new OAuthTokenCredential(_config).GetAccessToken();

            var apiContext = new APIContext
            {
                AccessToken = _accessToken,
                Config = ConfigManager.Instance.GetProperties()
            };

            var paymentExecution = new PaymentExecution { payer_id = payerId };
            var payment = new Payment { id = paymentId };
            var executedPayment = await Task.Run(() => payment.Execute(apiContext, paymentExecution));
            return executedPayment;
        }
    }
}
