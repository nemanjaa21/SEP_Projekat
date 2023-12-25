﻿using BitcoinPaymentService.Interfaces;
using BitcoinPaymentService.Models;
using System.Runtime.InteropServices;

namespace BitcoinPaymentService.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Transaction> GetByPaymentId(string paymentId)
        {
            Transaction? transaction = await _unitOfWork.TransactionsRepository.Get(x => x.PaymentId == paymentId);
            if (transaction == null)
                throw new Exception($"Transaction with payment id {paymentId} doesn't exist.");

            return transaction;
        }

        public async Task<Transaction> GetById(int id)
        {
            Transaction? transaction = await _unitOfWork.TransactionsRepository.Get(x => x.Id == id);
            if (transaction == null)
                throw new Exception($"Transaction with id {id} doesn't exist.");

            return transaction;
        }

        public async Task<Transaction> MakeTransaction(int merchantId, int userId)
        {
            Transaction transaction = new Transaction
            {
                Amount = 100.0m,
                MerchantOrderId = 123456,
                MerchantTimestamp = DateTime.Now,
                IdUser = userId,
                IdMerchant = merchantId

            };
            await _unitOfWork.TransactionsRepository.Insert(transaction);
            await _unitOfWork.Save();
            return transaction;
        }
    }
}
