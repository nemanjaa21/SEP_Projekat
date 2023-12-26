namespace shared
{
    public class PaymentResponse
    {
        public string? PaymentUrl { get; set; }
        public string? PaymentId { get; set; }
        public PaymentResponse(string queueName)
        {
            if(queueName.Equals("Credit_Card_Payment"))
            {
                PaymentUrl = "http://localhost:3000/bank/card";
                PaymentId = "5b708c40-3a20-47aa-8fff-4b5640062784";
            }
            else
            {
                PaymentUrl = "http://localhost:3000/bank/qrcode";
                PaymentId = "4f209d87-4496-43a3-8253-4195dc141d4e";
            }
        }
        public PaymentResponse(string? paymentUrl, string? paymentId)
        {
            PaymentUrl = paymentUrl;
            PaymentId = paymentId;
        }
    }
}
