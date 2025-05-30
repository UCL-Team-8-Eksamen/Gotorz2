﻿namespace Gotorz.Client.Models
{
    public class Accommodation
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
        public string availableRoomsStatus { get; set; }
        public int lengthOfStay { get; set; }
        public string roomType { get; set; }
        public string roomTypeDescription { get; set; }
        public string PictureUrl { get; set; }
    }

}
