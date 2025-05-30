﻿//Bruger vi det til noget eller skal det slettes?
namespace Gotorz.Client.Models
{
    public class Travel
    {
        public string TransportNumber { get; set; }
        public string TransportationCompany { get; set; }

        public string DepartureCountry { get; set; }
        public string DepartureCity { get; set; }
        public string DepartureLocation { get; set; }
        public DateTime DepartureTime { get; set; }

        public string ArrivalCountry { get; set; }
        public string ArrivalCity { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime ArrivalTime { get; set; }

        public TimeSpan TravelDuration { get; set; }
        public decimal TotalPrice { get; set; }
        public string TicketClass { get; set; }

        public int NumberOfSeats { get; set; }
        public string BagageType { get; set; }
        public int NumberOfBagageItems { get; set; }

        public string UserName { get; set; } //Test - Login
    }
}