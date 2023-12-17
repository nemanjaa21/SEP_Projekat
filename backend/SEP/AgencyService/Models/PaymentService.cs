using Common.Models;

namespace AgencyService.Models
{
    public class PaymentService : PaymentServiceEB
    {
        public List<Agency>? Agencies { get; set; }
    }
}
