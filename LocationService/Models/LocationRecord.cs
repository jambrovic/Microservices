using System;

namespace LocationService.Models
{
    public class LocationRecord
    {
        public Guid ID { get; set; }
        public float Latitude { get; set; }
        public long Longitude { get; set; } 
        public long Altitude { get; set; }
        public long Timestamp { get; set; }
        public Guid MemberID { get; set; }

    }
}