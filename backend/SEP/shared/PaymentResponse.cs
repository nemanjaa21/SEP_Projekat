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
                PaymentId = "84dc26ad-6cb2-4944-9a75-f81fb8e70fc0";
            }
            else
            {
                PaymentUrl = "http://localhost:3000/bank/qrcode";
                PaymentId = "db6a132c-0b92-484e-b99f-8534043661dc";
            }
        }
        public PaymentResponse(string? paymentUrl, string? paymentId)
        {
            PaymentUrl = paymentUrl;
            PaymentId = paymentId;
        }
    }
}
