namespace BankService.DTO
{
    public class PSPResponseDTO
    {
        public string? Url { get; set; }
        public long AcquirerOrderId { get; set; }
        public DateTime AcquirerTimestamp { get;  set; }
        public long MerchantOrderId { get; set; }
        public string? PaymentId { get; set; }
        public PSPResponseDTO()
        {
        }
        public PSPResponseDTO(string? url, long acquirerOrderId, DateTime acquirerTimestamp, long merchantOrderId, string? paymentId)
        {
            Url = url;
            AcquirerOrderId = acquirerOrderId;
            AcquirerTimestamp = acquirerTimestamp;
            MerchantOrderId = merchantOrderId;
            PaymentId = paymentId;
        }
    }
}
