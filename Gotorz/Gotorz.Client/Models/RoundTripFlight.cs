namespace Gotorz.Client.Models
{
    // Klassen repræsenterer en returbillet-flyvning (altså en tur-retur flyrejse)
    // RoundTripFlight-klassen har ansvaret for at gemme oplysninger om en tur-retur-flyvning
    // Den hjælper med at samle begge flyvninger og totalprisen i ét samlet objekt.
    public class RoundTripFlight
    {
        //Repræsenterer udrejsen.FlightInfo er en anden klasse, som indeholder detaljer om en flyvning
        //Vi laver automatisk et tomt objekt, så det ikke er null
        public FlightInfo Outbound { get; set; } = new FlightInfo();

        //Repræsenterer udrejsen.FlightInfo er en anden klasse, som indeholder detaljer om en flyvning
        //Vi laver automatisk et tomt objekt, så det ikke er null
        public FlightInfo Inbound { get; set; } = new FlightInfo();

        // Totalprisen for rejsen
        public string TotalPrice { get; set; } = string.Empty;
    }
}
