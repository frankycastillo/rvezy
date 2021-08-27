using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace rvezy.Data
{
    public abstract class BaseModel : IBaseModel
    {
        protected BaseModel()
        {
            CreatedOn = DateTimeOffset.UtcNow;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public Guid Id { get; set; }
        
        [DataMember]
        public DateTimeOffset? CreatedOn { get; set; }

        [DataMember]
        public Guid? CreatedBy { get; set; }
        
        [DataMember]
        public DateTimeOffset? ModifiedOn { get; set; }
        
        [DataMember]
        public Guid? ModifiedBy { get; set; }
        
        [DataMember]
        public bool Deleted { get; set; }
        
        [DataMember]
        public DateTimeOffset? DeletedOn { get; set; }
        
        [DataMember]
        public Guid? DeletedBy { get; set; }
    }
}