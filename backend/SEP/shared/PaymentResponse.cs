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
                PaymentId = "5dec46b5-6a7a-4016-b2d8-181eb6b1c3f4";
            }
            else
            {
                PaymentUrl = "http://localhost:3000/bank/qrcode";
                PaymentId = "1304e4dd-357d-4233-ad4a-412c8a4b1da8";
            }
        }
        public PaymentResponse(string? paymentUrl, string? paymentId)
        {
            PaymentUrl = paymentUrl;
            PaymentId = paymentId;
        }
    }
}
