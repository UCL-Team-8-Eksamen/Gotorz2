namespace Gotorz2.Client.Models
{
    public class RoundTripFlight
    {
        public FlightInfo Outbound { get; set; } = new FlightInfo();
        public FlightInfo Inbound { get; set; } = new FlightInfo();
        public string TotalPrice { get; set; } = string.Empty;
    }
}
