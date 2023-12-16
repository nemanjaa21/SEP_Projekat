namespace BankService.DTO
{
    public class PSPResponseDTO
    {
        public string? Url { get; private set; }
        public long AcquirerOrderId { get; private set; }
        public DateTime AcquirerTimestamp { get; private set; }
        public long MerchantOrderId { get; private set; }
        public string? PaymentId { get; private set; }
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
