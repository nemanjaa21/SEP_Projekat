using BankService.Interfaces;
using BankService.Models;

namespace BankService.Services
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Card> CheckCardInfo(Card card)
        {
            var verifiedCard = await _unitOfWork.CardsRepository.Get(c => c.Pan == card.Pan && c.CardHolderName == card.CardHolderName && c.SecurityCode == card.SecurityCode && c.ExpirationDate == card.ExpirationDate);
            if (verifiedCard == null)
                throw new Exception("No card with given data was found.");

            return verifiedCard;
        }
    }
}
