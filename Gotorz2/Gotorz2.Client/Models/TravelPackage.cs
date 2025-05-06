namespace Gotorz2.Client.Models
{

    public class TravelPackage
    {
        public string TripTitle { get; set; } = string.Empty;
        public RoundTripFlight Flight { get; set; }
        public AccommodationData Hotel { get; set; }
        public string Description { get; set; }
        public decimal TripTotalPrice { get; set; }
    }


}
