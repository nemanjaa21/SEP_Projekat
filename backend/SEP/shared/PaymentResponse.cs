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
                PaymentId = "724513e7-4e11-419a-964e-58957cb85334";
            }
            else
            {
                PaymentUrl = "http://localhost:3000/bank/qrcode";
                PaymentId = "eac7d64e-e4c0-455a-9b79-60ef83275886";
            }
        }
        public PaymentResponse(string? paymentUrl, string? paymentId)
        {
            PaymentUrl = paymentUrl;
            PaymentId = paymentId;
        }
    }
}
