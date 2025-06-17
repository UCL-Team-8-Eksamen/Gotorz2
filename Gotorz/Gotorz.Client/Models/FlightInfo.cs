namespace Gotorz.Client.Models
{

    //Klassen FlightInfo har ansvaret for at repræsentere information om én enkelt flyrejse – altså enten en udrejse eller en hjemrejse.
    public class FlightInfo
    {
        public string DepartureAirport { get; set; } = string.Empty;
        public string ArrivalAirport { get; set; } = string.Empty;
        public string DepartureTime { get; set; } = string.Empty;
        public string ArrivalTime { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
    }
}
