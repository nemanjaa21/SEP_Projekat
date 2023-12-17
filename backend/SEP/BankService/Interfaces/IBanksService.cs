using BankService.DTO;
using BankService.Models;

namespace BankService.Interfaces
{
    public interface IBanksService
    {
        bool IsSameBank(string pan);
        Task<PCCResponseDTO> SendToPCC(CardInfoDTO cardInfoDTO, Transaction transaction);
        Task<PCCResponseDTO> ResendToPCC(PCCRequestDTO pccRequestDTO, string issuerAccountNumber, int userId);
        Task<PSPResponseDTO> SendToPSP(CardInfoDTO cardInfoDTO, int issuerId, Transaction transaction);
        Task<PSPResponseDTO> SendToPSPFromPCC(PCCResponseDTO pccResponseDTO);
    }
}
