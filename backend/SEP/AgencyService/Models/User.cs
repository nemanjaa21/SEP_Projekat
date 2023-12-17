using Common.Models;

namespace AgencyService.Models
{
    public class User : UserEB
    {
        public Agency Agency { get; set; }
        public int AgencyId { get; set; }
    }
}
