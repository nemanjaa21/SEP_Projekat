using BankService.DTO;
using BankService.Interfaces;
using BankService.Models;
using System;

namespace BankService.Services
{
    public class BanksService : IBanksService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICardService _cardService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly IConfiguration _configuration;
        private string successUrl;
        private string bankId;

        public BanksService(IUnitOfWork unitOfWork, ICardService cardService, IAccountService accountService, ITransactionService transaction, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _cardService = cardService;
            _accountService = accountService;
            _transactionService = transaction;
            _configuration = configuration;

            successUrl = _configuration["URLS:SUCCESS_URL"];
            bankId = _configuration["BANK:ID"];
        }

        public bool IsSameBank(string pan)
        {
            return pan.StartsWith(bankId);
        }
        public async Task<PCCResponseDTO> SendToPCC(CardInfoDTO cardInfoDTO, Transaction transaction)
        {
            Random random = new Random();
            string accountNumber = await _accountService.GetAccountNumberByMerchant(transaction.IdMerchant);
            long aciquiererOrderId = (long)(random.NextDouble() * 1000000);
            DateTime aciquiererTimeStamp = DateTime.Now;
            PCCRequestDTO pccRequestDTO = new PCCRequestDTO(cardInfoDTO.Pan, cardInfoDTO.SecurityCode, cardInfoDTO.CardHolderName, cardInfoDTO.ExpirationDate, transaction.Amount,
                aciquiererOrderId, aciquiererTimeStamp, bankId, transaction.MerchantOrderId, transaction.MerchantTimestamp, transaction.PaymentId, accountNumber);

            transaction.AcquirerOrderId = aciquiererOrderId;
            transaction.AcquirerTimestamp = aciquiererTimeStamp;
            transaction.AcquirerAccountNumber = accountNumber;

            _unitOfWork.TransactionsRepository.Update(transaction);
            await _unitOfWork.Save();

            //SEND TO PCC AND RETRIEVE BACK.

            return null;
        }

        public Task<PCCResponseDTO> ResendToPCC(PCCRequestDTO pccRequestDTO, string accountNumber, long userId)
        {
            throw new NotImplementedException();
        }

        public Task<PSPResponseDTO> SendToPSP(CardInfoDTO cardInfoDTO, long issuerId, Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task<PSPResponseDTO> SendToPSPFromPCC(PCCResponseDTO pccResponseDTO)
        {
            throw new NotImplementedException();
        }
    }
}
