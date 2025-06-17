using System.ComponentModel.DataAnnotations;

namespace Gotorz.Client.Models
{
    public class TravelPackage
    {
        [Key] // Marker primærnøgle hvis du bruger EF Core
        public int TravelPackageID { get; set; }

        [Required(ErrorMessage = "Trip Title er påkrævet")]
        [StringLength(100, ErrorMessage = "Trip Title må max være 100 tegn")]
        public string TripTitle { get; set; } = string.Empty;

        [Url(ErrorMessage = "PictureUrl skal være en gyldig URL")]
        public string? PictureUrl { get; set; }

        [Required(ErrorMessage = "Flight er påkrævet")]
        public RoundTripFlight Flight { get; set; }

        [Required(ErrorMessage = "Hotel er påkrævet")]
        public Accommodation Hotel { get; set; }

        [Required(ErrorMessage = "Description er påkrævet")]
        [StringLength(500, ErrorMessage = "Beskrivelsen må max være 500 tegn")]
        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "TripTotalPrice skal være positiv")]
        public decimal TripTotalPrice { get; set; }
    }
}
