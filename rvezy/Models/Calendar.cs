using System;
using System.ComponentModel.DataAnnotations.Schema;
using rvezy.Data;

namespace rvezy.Models
{
    [Table("Calendar")]
    public class Calendar : BaseModel
    {
        [ForeignKey("Listing")]
        public Guid ListingId { get; set; }

        [NotMapped]
        public Listing Listing { get; set; }

        public DateTime EntryDate { get; set; }
        public bool Available { get; set; }
        public double Price { get; set; }
    }
}