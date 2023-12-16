using PaymentServiceProvider.Interfaces;
using PaymentServiceProvider.Models;
using shared;

namespace PaymentServiceProvider.Services
{
    public class PaymentServiceImpl : IPaymentService
    {
        private IUnitOfWork _unitOfWork;
        private IMerchantService _merchantService;

        public PaymentServiceImpl(IUnitOfWork unitOfWork, IMerchantService merchantService)
        {
            _unitOfWork = unitOfWork;
            _merchantService = merchantService;
        }

        public async Task<List<PaymentService>> GetAllPaymentMethods()
        {
            var paymentMethods = await _unitOfWork.PaymentServiceRepository.GetAll();
            return paymentMethods.ToList();
        }

        public async Task<PaymentService> GetPaymentService(int id)
        {
            var paymentService = await _unitOfWork.PaymentServiceRepository.Get(x => x.Id == id);
            if (paymentService == null)
                throw new Exception("Payment service isn't valid");

            return paymentService;
        }

        public async Task<PaymentResponse> ProcessPayment(PSPRequest pspRequest, string apiKey)
        {
            try
            {
                Random random = new Random();
                Merchant? merchant = await _merchantService.GetMercantByApiKey(apiKey);
                PaymentService? paymentType = await GetPaymentService(pspRequest.PaymentTypeId);

                PaymentRequest paymentRequest = new PaymentRequest(merchant.MerchantId, merchant.MerchantPassword, pspRequest.Amount, random.NextInt64(), DateTime.Now, "SUCCESS_URL", "FAIL_URL", "ERROR_URL");              

                //OVE URLOVE TREBA POVUCI IZ CONFIG-A VRV, I OVDE SE NEKIM BROOKEROM SALJE NA MIKROSERVISE.
                //PaymentResponse = brooker.sendRequest(paymentType, paymentRequest)

                return new PaymentResponse(null, "SUCCESS_URL");
            }
            catch (Exception)
            {
                return new PaymentResponse(null, "FAIL_URL");
            }
        }
    }
}
