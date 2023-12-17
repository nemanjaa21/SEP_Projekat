using BankService.DTO;
using BankService.Enums;
using BankService.Interfaces;
using BankService.Models;
using shared;

namespace BankService.Services
{
    public class PSPService : IPSPService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMerchantService _merchantService;
        private readonly IConfiguration _configuration;
        private string paymentUrl;
        private string successUrl;
        private string failUrl;
        private string errorUrl;

        public PSPService(IUnitOfWork unitOfWork, IMerchantService merchantService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _merchantService = merchantService;
            _configuration = configuration;

            paymentUrl = _configuration["URLS:PAYMENT_URL"];
            successUrl = _configuration["URLS:SUCCESS_URL"];
            failUrl = _configuration["URLS:FAIL_URL"];
            errorUrl = _configuration["URLS:ERROR_URL"];
        }

        public async Task<PaymentResponse> ProcessPayment(PaymentRequest paymentRequest)
        {
            try
            {
                Merchant? merchant = await _merchantService.GetByMerchantId(paymentRequest.MerchantId!);
                string paymentId = Guid.NewGuid().ToString();

                Transaction transaction = new Transaction()
                {
                    Amount = paymentRequest.Amount,
                    IdMerchant = merchant.Id,
                    Merchant_Id = paymentRequest.MerchantId,
                    MerchantOrderId = paymentRequest.MerchantOrderId,
                    MerchantTimestamp = paymentRequest.MerchantTimestamp,
                    Status = Enums.Status.PENDING,
                    PaymentId = paymentId,
                };

                await _unitOfWork.TransactionsRepository.Insert(transaction);
                await _unitOfWork.Save();

                return new PaymentResponse(paymentId, paymentUrl);
            }
            catch (Exception)
            {
                return new PaymentResponse(null, failUrl);
            }
        }

        public PSPResponseDTO GenerateResponseBasedOnURL(Url url)
        {
            if (url == Url.SUCCESSFUL)
                return new PSPResponseDTO() { Url = successUrl};
            else if (url == Url.FAILED)
                return new PSPResponseDTO() { Url = failUrl };
            else
                return new PSPResponseDTO() { Url = errorUrl };
        }
    }
}
