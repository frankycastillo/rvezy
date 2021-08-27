using System;
using System.ComponentModel.DataAnnotations.Schema;
using rvezy.Data;

namespace rvezy.Models
{
    [Table("Review")]
    public class Review : BaseModel
    {
        [ForeignKey("Listing")] 
        public Guid ListingId { get; set; }

        [NotMapped] 
        public Listing Listing { get; set; }

        public DateTime ReviewDate { get; set; }

        public string ReviewerId { get; set; }

        public string ReviewerName { get; set; }

        public string Comments { get; set; }
    }
}