using BankService.Interfaces;
using BankService.Models;

namespace BankService.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MerchantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Merchant> GetByMerchantId(int id)
        {
            var merchant = await _unitOfWork.MerchantsRepository.Get(m => m.Id == id);
            if (merchant == null)
                throw new Exception($"Merchant with {id} not found!");

            return merchant;
        }

        public async Task<Merchant> GetByMerchantId(string id)
        {
            var merchant = await _unitOfWork.MerchantsRepository.Get(m => m.Merchant_Id == id);
            if (merchant == null)
                throw new Exception($"Merchant with {id} not found!");

            return merchant;
        }
    }
}
