using BankService.DTO;
using BankService.Interfaces;
using BankService.Models;
using Shared;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace BankService.Services
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AESCryptoService _AESCryptoService;
        private readonly IConfiguration _configuration;
        byte[] byte_key;

        public CardService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _AESCryptoService = new AESCryptoService();
            _configuration = configuration;
            byte_key = Encoding.ASCII.GetBytes(configuration["AES_KEY"]);
        }

        public async Task<Card> CheckCardInfo(CardInfoDTO cardInfoDTO)
        {

            var verifiedCard = await _unitOfWork.CardsRepository.Get(c => c.ExpirationDate == cardInfoDTO.ExpirationDate && c.CardHolderName == cardInfoDTO.CardHolderName, new List<string>() { "Account" });
            if (cardInfoDTO.Pan != _AESCryptoService.Decrypt(verifiedCard!.Pan!, byte_key) || cardInfoDTO.SecurityCode != _AESCryptoService.Decrypt(verifiedCard!.SecurityCode!, byte_key))
                throw new Exception("No card with given data was found.");

            return verifiedCard;
        }

        public async Task AddNewCard(NewCardDTO newCardDTO)
        {
            Card card = new Card()
            {
                Pan = _AESCryptoService.Encrypt(newCardDTO.Pan!, byte_key),
                SecurityCode = _AESCryptoService.Encrypt(newCardDTO.SecurityCode!, byte_key),
                CardHolderName = newCardDTO.CardHolderName,
                ExpirationDate = newCardDTO.ExpirationDate,
                AccountId = newCardDTO.AccountId
            };

            var existingCard = await _unitOfWork.CardsRepository.Get(c => c.Pan == card.Pan && c.SecurityCode == card.SecurityCode, new List<string>() { "Account" });
            if (existingCard != null)
                throw new Exception("This card already exists");

            await _unitOfWork.CardsRepository.Insert(card);
            await _unitOfWork.Save();
        }
    }
}
