﻿using PaymentServiceProvider.Interfaces;
using PaymentServiceProvider.Models;

namespace PaymentServiceProvider.Services
{
    public class MerchantServiceImpl : IMerchantService
    {
        private IUnitOfWork _unitOfWork;

        public MerchantServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GenerateApiKey(string merchantId)
        {
            Merchant? merchant = await _unitOfWork.MerchantRepository.Get(x => x.MerchantId == merchantId);
            if (merchant == null)
                throw new Exception($"Merchant with id {merchantId} not found.");

            merchant.ApiKey = merchantId + DateTime.Now.Ticks.ToString();

            _unitOfWork.MerchantRepository.Update(merchant);
            await _unitOfWork.Save();

            return merchant.ApiKey;
        }

        public async Task<Merchant> GetMercantByApiKey(string apiKey)
        {
            Merchant? merchant = await _unitOfWork.MerchantRepository.Get(x => x.ApiKey == apiKey);
            if (merchant == null)
                throw new Exception($"Merchant with api {apiKey} not found.");

            return merchant;
        }

        public async Task<Merchant> GetMerchantById(string merchantId)
        {
            Merchant? merchant = await _unitOfWork.MerchantRepository.Get(x => x.MerchantId == merchantId);
            return merchant!;
        }

        public async Task<string> RegisterMerchant(Merchant newMerchant)
        {
            Merchant? merchant = await GetMerchantById(newMerchant.MerchantId!);
            if (merchant != null)
                throw new Exception($"Merchant with id {newMerchant.MerchantId} is already registered.");

            newMerchant.MerchantPassword = BCrypt.Net.BCrypt.HashPassword(newMerchant.MerchantPassword);

            await _unitOfWork.MerchantRepository.Insert(newMerchant);
            await _unitOfWork.Save();
            
            return await GenerateApiKey(newMerchant.MerchantId!);
        }
    }
}
