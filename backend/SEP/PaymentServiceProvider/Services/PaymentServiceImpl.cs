using PaymentServiceProvider.Interfaces;
using PaymentServiceProvider.Models;

namespace PaymentServiceProvider.Services
{
    public class PaymentServiceImpl : IPaymentService
    {
        private IUnitOfWork _unitOfWork;

        public PaymentServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PaymentService>> GetAllPaymentMethods()
        {
            var paymentMethods = await _unitOfWork.PaymentServiceRepository.GetAll();
            return paymentMethods.ToList();
        }
    }
}
