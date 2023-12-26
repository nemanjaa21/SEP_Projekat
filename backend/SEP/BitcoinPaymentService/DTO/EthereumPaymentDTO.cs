namespace BitcoinPaymentService.DTO
{
    public class EthereumPaymentDTO
    {
        public string? To { get; set; }
        public string? Value { get; set; }
        public string? Input { get; set; }
        public int? TransactionId { get; set; }
    }
}
