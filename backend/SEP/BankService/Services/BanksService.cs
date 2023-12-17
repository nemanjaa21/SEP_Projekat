using BankService.DTO;
using BankService.Enums;
using BankService.Interfaces;
using BankService.Models;
using Newtonsoft.Json;
using System;
using System.Text;

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
        HttpClient _httpClient;

        public BanksService(IUnitOfWork unitOfWork, ICardService cardService, IAccountService accountService, ITransactionService transaction, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _cardService = cardService;
            _accountService = accountService;
            _transactionService = transaction;
            _configuration = configuration;

            successUrl = _configuration["URLS:SUCCESS_URL"];
            bankId = _configuration["BANK:ID"];
            _httpClient = new HttpClient();
        }

        public bool IsSameBank(string pan)
        {
            return pan.StartsWith(bankId);
        }
        public async Task<PCCResponseDTO> SendToPCC(CardInfoDTO cardInfoDTO, Transaction transaction)
        {
            Random random = new Random();
            string acquirerAccountNumber = await _accountService.GetAccountNumberByMerchant(transaction.IdMerchant);
            long acquirerOrderId = (long)(random.NextDouble() * 1000000);
            DateTime acquirerTimeStamp = DateTime.Now;

            PCCRequestDTO pccRequestDTO = new PCCRequestDTO(cardInfoDTO.Pan, cardInfoDTO.SecurityCode, cardInfoDTO.CardHolderName, cardInfoDTO.ExpirationDate, transaction.Amount,
                acquirerOrderId, acquirerTimeStamp, bankId, transaction.MerchantOrderId, transaction.MerchantTimestamp, transaction.PaymentId, acquirerAccountNumber);

            transaction.AcquirerOrderId = acquirerOrderId;
            transaction.AcquirerTimestamp = acquirerTimeStamp;
            transaction.AcquirerAccountNumber = acquirerAccountNumber;

            _unitOfWork.TransactionsRepository.Update(transaction);
            await _unitOfWork.Save();

            //SEND TO PCC AND RETRIEVE BACK.
            try
            {
                string jsonRequest = JsonConvert.SerializeObject(pccRequestDTO);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:7241/api/PCC/ToIssuerBank", content);

                string responseBody = await response.Content.ReadAsStringAsync();
                PCCResponseDTO? pccResponseDTO = JsonConvert.DeserializeObject<PCCResponseDTO>(responseBody);
                return pccResponseDTO!;
            }
            catch (Exception ex)
            {                
                Console.WriteLine("PCC Request Exception: " + ex.Message);
                return null!;
            }
        }

        public async Task<PCCResponseDTO> ResendToPCC(PCCRequestDTO pccRequestDTO, string issuerAccountNumber, int userId)
        {
            Random random = new Random();
            long issuerOrderId = (long)(random.NextDouble() * 1000000);
            DateTime issuerTimeStamp = DateTime.Now;

            PCCResponseDTO pccResponse = new PCCResponseDTO(pccRequestDTO.Pan, pccRequestDTO.SecurityCode, pccRequestDTO.CardHolderName, pccRequestDTO.CardHolderName, pccRequestDTO.Amount, pccRequestDTO.AcquirerOrderId, pccRequestDTO.AcquirerTimestamp,
                issuerOrderId, issuerTimeStamp, bankId, pccRequestDTO.MerchantOrderId, pccRequestDTO.MerchantTimestamp, pccRequestDTO.PaymentId, pccRequestDTO.AcquirerAccountNumber, issuerAccountNumber);

            Transaction transaction = await _transactionService.GetByPaymentId(pccRequestDTO.PaymentId!);

            transaction.IdUser = userId;
            transaction.IssuerOrderId = issuerOrderId;
            transaction.IssuerTimestamp = issuerTimeStamp;
            transaction.IssuerAccountNumber = issuerAccountNumber;

            _unitOfWork.TransactionsRepository.Update(transaction);
            await _unitOfWork.Save();

            return pccResponse;
        }

        public async Task<PSPResponseDTO> SendToPSP(CardInfoDTO cardInfoDTO, int issuerId, Transaction transaction)
        {
            Random random = new Random();
            string issuerAccountNumber = await _accountService.GetAccountNumberByUser(issuerId);
            string acquirerAccountNumber = await _accountService.GetAccountNumberByMerchant(transaction.IdMerchant);
            long acquirerOrderId = (long)(random.NextDouble() * 1000000);
            DateTime acquirerTimeStamp = DateTime.Now;

            PSPResponseDTO pspResponse = new PSPResponseDTO(successUrl, acquirerOrderId, acquirerTimeStamp, transaction.MerchantOrderId, transaction.PaymentId);

            transaction.AcquirerOrderId = acquirerOrderId;
            transaction.AcquirerTimestamp = acquirerTimeStamp;
            transaction.AcquirerAccountNumber = acquirerAccountNumber;
            transaction.IdUser = issuerId;
            transaction.IssuerAccountNumber = issuerAccountNumber;
            transaction.Status = Status.SUCCESSFUL;

            _unitOfWork.TransactionsRepository.Update(transaction);
            await _unitOfWork.Save();

            return pspResponse;
        }

        public async Task<PSPResponseDTO> SendToPSPFromPCC(PCCResponseDTO pccResponseDTO)
        {
            PSPResponseDTO pspResponse = new PSPResponseDTO(successUrl, pccResponseDTO.AcquirerOrderId, pccResponseDTO.AcquirerTimestamp, pccResponseDTO.MerchantOrderId, pccResponseDTO.PaymentId);

            Transaction transaction = await _transactionService.GetByPaymentId(pccResponseDTO.PaymentId!);
            transaction.IssuerOrderId = pccResponseDTO.IssuerOrderId;
            transaction.IssuerTimestamp = pccResponseDTO.IssuerTimestamp;
            transaction.IssuerAccountNumber = pccResponseDTO.IssuerAccountNumber;
            transaction.Status = Status.SUCCESSFUL;

            _unitOfWork.TransactionsRepository.Update(transaction);
            await _unitOfWork.Save();

            return pspResponse;
        }
    }
}
