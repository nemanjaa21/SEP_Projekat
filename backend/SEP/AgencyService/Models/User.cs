namespace AgencyService.Models
{
    public class User : EntityBase
    {
        public Agency Agency { get; set; }
        public int AgencyId { get; set; }
    }
}
