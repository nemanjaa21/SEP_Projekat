﻿namespace BankService.Interfaces
{
    public interface IAccountService
    {
        Task<int> GetAccountId(string acquirerAccountNumber);
        Task <string> GetAccountNumberByMerchant(int merchantId);
        Task <string> GetAccountNumberByUser(int userId);
        Task <bool> WithdrawMoney(int userId, decimal amount);
        Task <bool> DepositMoney(int merchantId, decimal amount);
        Task<bool> WithdrawMoneyViaAccount(string userAccount, decimal amount);
        Task<bool> DepositMoneyViaAccount(string merchantAccount, decimal amount);
    }
}
