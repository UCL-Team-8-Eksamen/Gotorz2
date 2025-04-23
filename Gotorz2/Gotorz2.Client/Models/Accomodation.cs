namespace Gotorz2.Client.Models
{
    public class Accomodation
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string AccomodationName { get; set; }
        public string RoomType { get; set; }
        public string RoomTypeDescription { get; set; }
        public int NumberOfRooms { get; set; }
        public string AvailableRoomStatus { get; set; }
        public decimal PricePerNight { get; set; }
        public int StarRating { get; set; }
        public string Facilities { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int LengthOfStay => (CheckOutDate - CheckInDate).Days; //Should we calculate this from the CheckIn and CheckOut dates?
        public string FoodPlan { get; set; }
        public string AccomodationImageUrl { get; set; } // To show an image in the frontend

    }
}
