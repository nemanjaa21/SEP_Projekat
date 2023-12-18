using PaymentCardCenterService.DTO;

namespace PaymentCardCenterService.Interfaces
{
    public interface IPCCService
    {
        Task <PCCResponseDTO> ForwardToIssuerBank(PCCRequestDTO pccRequestDTO);
    }
}
