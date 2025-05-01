namespace Gotorz2.Client.Models
{
    public class AccommodationData
    {
        public string country { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string accommodationName { get; set; }
        public int pricePerNight { get; set; }
        public int starRating { get; set; }
        public DateTime checkInDate { get; set; }
        public DateTime checkOutDate { get; set; }
        public List<string> facilities { get; set; }
        public string accommodationImageUrl { get; set; }
        public string availableRoomsStatus { get; set; }
        public int lengthOfStay { get; set; }
        public string roomType { get; set; }
        public string roomTypeDescription { get; set; }
    }
    //public class AccommodationData
    //{
    //    public string Name { get; set; }
    //    public Address Address { get; set; }
    //    public RatePlan RatePlan { get; set; }
    //    public int? StarRating { get; set; }
    //    public List<Facility> Facilities { get; set; }
    //    public List<Picture> Pictures { get; set; }
    //}

    //public class Address
    //{
    //    public string Line { get; set; }
    //    public string City { get; set; }
    //    public string Country { get; set; }
    //}

    //public class RatePlan
    //{
    //    public Price Price { get; set; }
    //}

    //public class Price
    //{
    //    public decimal Total { get; set; }
    //}

    //public class Facility
    //{
    //    public string Name { get; set; }
    //}

    //public class Picture
    //{
    //    public string Uri { get; set; }
    //}
}
