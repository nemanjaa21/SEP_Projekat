namespace shared
{
    public class PaymentResponse
    {
        public string? PaymentUrl { get; set; }
        public string? PaymentId { get; set; }
        public PaymentResponse()
        {
            PaymentUrl = "http://localhost:3000/bank/card";
            PaymentId = "5b708c40-3a20-47aa-8fff-4b5640062784";
        }
        public PaymentResponse(string? paymentUrl, string? paymentId)
        {
            PaymentUrl = paymentUrl;
            PaymentId = paymentId;
        }
    }
}
