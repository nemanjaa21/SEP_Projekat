﻿namespace AgencyService.Models
{
    public class ServiceOffer: EntityBase
    {
        public List<ServiceOfferItem>? ServiceOfferItems { get; set; }
        public double TotalPrice { get; set; }
    }
}
