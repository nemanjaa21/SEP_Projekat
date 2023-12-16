using BankService.Models;

namespace BankService.Interfaces
{
    public interface ICardService
    {
        public Task<Card> CheckCardInfo(Card card);
    }
}
