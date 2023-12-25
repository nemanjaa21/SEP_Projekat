using BankService.DTO;
using BankService.Models;

namespace BankService.Interfaces
{
    public interface IBanksService
    {
        bool IsSameBank(string pan);
        Task<PCCResponseDTO> SendToPCC(CardInfoDTO cardInfoDTO, Transaction transaction);
        Task<PCCResponseDTO> ResendToPCC(PCCRequestDTO pccRequestDTO, string issuerAccountNumber, int userId);
        Task<PSPResponseDTO> SendToPSP(int issuerId, Transaction transaction);
        Task<PSPResponseDTO> SendToPSPFromPCC(PCCResponseDTO pccResponseDTO);
        Task<PSPResponseDTO> UpdateTransaction(string issuerAccountNumber, string merchantAccountNumber, int userId, Transaction transaction);
    }
}
