using System;
using System.ComponentModel.DataAnnotations;

namespace rvezy.Models
{
    public class Calendar
    {
        [Key]
        public int listing_id { get; set; }
        public DateTime date { get; set; }
        public bool available { get; set; }
        public Double price { get; set; }
    }
}