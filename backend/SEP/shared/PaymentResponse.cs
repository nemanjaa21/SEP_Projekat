namespace shared
{
    public class PaymentResponse
    {
        public string? PaymentUrl { get; set; }
        public string? PaymentId { get; set; }

        public PaymentResponse(string? paymentUrl, string? paymentId)
        {
            PaymentUrl = paymentUrl;
            PaymentId = paymentId;
        }
    }
}
