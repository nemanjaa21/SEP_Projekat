using BankService.DTO;
using BankService.Models;

namespace BankService.Interfaces
{
    public interface ICardService
    {
        public Task<Card> CheckCardInfo(CardInfoDTO card);
        public Task AddNewCard(NewCardDTO newCardDTO);
    }
}