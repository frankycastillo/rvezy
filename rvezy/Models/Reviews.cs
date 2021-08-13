using System;
using System.ComponentModel.DataAnnotations;

namespace rvezy.Models
{
    public class Reviews
    {
        [Key]
        public int listing_id { get; set; }
        public string id { get; set; }
        public DateTime date { get; set; }
        public string reviewer_id { get; set; }
        public string reviewer_name { get; set; }
        public string comments { get; set; }
    }
}