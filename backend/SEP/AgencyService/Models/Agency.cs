using Common.Models;

namespace AgencyService.Models
{
    public class Agency : AgencyEB
    {
        public List<PaymentService>? PaymentServices { get; set; }
        public List<User>? Users { get; set; }
    }
}
