using BankService.Interfaces;
using BankService.Models;

namespace BankService.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> GetAccountId(string acquirerAccountNumber)
        {
            var account = await _unitOfWork.AccountsRepository.Get(account => account.AccountNumber == acquirerAccountNumber);
            return account?.Id ?? -1;
        }

        public async Task<string> GetAccountNumberByUser(int userId)
        {
            var account = await _unitOfWork.AccountsRepository.Get(account => account.UserId == userId);
            return account?.AccountNumber ?? string.Empty;
        }

        public async Task<string> GetAccountNumberByMerchant(int merchantId)
        {
            var account = await _unitOfWork.AccountsRepository.Get(account => account.MerchantId == merchantId);
            return account?.AccountNumber ?? string.Empty;
        }

        public async Task<bool> DepositMoney(int merchantId, decimal amount)
        {
            var accountReceiver = await _unitOfWork.AccountsRepository.Get(account => account.MerchantId == merchantId);
            if (accountReceiver != null && accountReceiver.Balance >= amount)
            {
                accountReceiver.Balance += amount;
                _unitOfWork.AccountsRepository.Update(accountReceiver);
                await _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public async Task<bool> WithdrawMoney(int userId, decimal amount)
        {
            var accountBuyer = await _unitOfWork.AccountsRepository.Get(account => account.UserId == userId);
            if (accountBuyer != null && accountBuyer.Balance >= amount)
            {
                accountBuyer.Balance -= amount;
                _unitOfWork.AccountsRepository.Update(accountBuyer);
                await _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public async Task<bool> DepositMoneyViaAccount(string merchantAccount, decimal amount)
        {
            var accountReceiver = await _unitOfWork.AccountsRepository.Get(account => account.AccountNumber == merchantAccount);
            if (accountReceiver != null && accountReceiver.Balance >= amount)
            {
                accountReceiver.Balance += amount;
                _unitOfWork.AccountsRepository.Update(accountReceiver);
                await _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public async Task<bool> WithdrawMoneyViaAccount(string userAccount, decimal amount)
        {
            var accountBuyer = await _unitOfWork.AccountsRepository.Get(account => account.AccountNumber == userAccount);
            if (accountBuyer != null && accountBuyer.Balance >= amount)
            {
                accountBuyer.Balance -= amount;
                _unitOfWork.AccountsRepository.Update(accountBuyer);
                await _unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
