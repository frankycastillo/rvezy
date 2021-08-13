using System;

namespace rvezy.Models
{
    public class Calendar
    {
        public int ListingId { get; set; }
        public DateTime Date { get; set; }
        public bool Available { get; set; }
        public Double Price { get; set; }
    }
}