﻿using BankService.Models;

namespace BankService.Interfaces
{
    public interface IAccountService
    {
        Task<int> GetAccountId(string acquirerAccountNumber);
        Task <string> GetAccountNumberByMerchant(int merchantId);
        Task <string> GetAccountNumberByUser(int userId);
        Task <bool> WithdrawMoney(int userId, double amount);
        Task <bool> DepositMoney(int merchantId, double amount);
    }
}