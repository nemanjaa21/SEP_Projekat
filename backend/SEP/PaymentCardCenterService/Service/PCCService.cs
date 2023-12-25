using Newtonsoft.Json;
using PaymentCardCenterService.DTO;
using PaymentCardCenterService.Interfaces;
using PaymentCardCenterService.Models;
using System.Text;

namespace PaymentCardCenterService.Service
{
    public class PCCService : IPCCService
    {
        private readonly IUnitOfWork _unitOfWork;
        HttpClient _httpClient;

        public PCCService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _httpClient = new HttpClient();
        }

        public async Task<PCCResponseDTO> ForwardToIssuerBank(PCCRequestDTO pccRequestDTO)
        {
            string bankUrl = await GetBankUrl(pccRequestDTO.Pan!);

            try
            {
                string jsonRequest = JsonConvert.SerializeObject(pccRequestDTO);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{bankUrl}/withdraw-money", content);

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

        private async Task<string> GetBankUrl(string pan)
        {
            string bankId = pan.Substring(0, 6);
            Bank? bank = await _unitOfWork.BankRepository.Get(x => x.BankId == bankId);
            return bank!.Url!;
        }
    }
}
