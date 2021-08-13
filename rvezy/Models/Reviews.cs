using System;

namespace rvezy.Models
{
    public class Reviews
    {
        public int ListingId { get; set; }
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string ReviewerIid { get; set; }
        public string ReviewerName { get; set; }
        public string Comments { get; set; }
    }
}