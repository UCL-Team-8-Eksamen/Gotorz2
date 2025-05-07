using Gotorz2.Client.Models;

//service som holder styr på vores valgte fly og hotel data
namespace Gotorz2.Client.Services
{

    public class TravelPackageState
    {
        public RoundTripFlight? SelectedFlight { get; set; }
        public AccommodationData? SelectedHotel { get; set; }

        public string? FlightOrigin { get; set; }
        public string? FlightDestination { get; set; }
        public DateTime? FlightDepartureDate { get; set; }
        public DateTime? FlightReturnDate { get; set; }
        public List<RoundTripFlight>? FlightSearchResults { get; set; }


        public string? HotelSearchCity { get; set; }
        public DateTime? HotelCheckIn { get; set; }
        public DateTime? HotelCheckOut { get; set; }
        public List<AccommodationData>? HotelSearchResults { get; set; }


        public List<TravelPackage> PublishedPackages { get; set; } = new();
    }

}



