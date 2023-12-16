using BankService.DTO;
using BankService.Models;

namespace BankService.Interfaces
{
    public interface IBanksService
    {
        bool IsSameBank(string pan);
        Task<PCCResponseDTO> SendToPCC(CardInfoDTO cardInfoDTO, Transaction transaction);
        Task<PCCResponseDTO> ResendToPCC(PCCRequestDTO pccRequestDTO, string accountNumber, long userId);
        Task<PSPResponseDTO> SendToPSP(CardInfoDTO cardInfoDTO, long issuerId, Transaction transaction);
        Task<PSPResponseDTO> SendToPSPFromPCC(PCCResponseDTO pccResponseDTO);
    }
}
