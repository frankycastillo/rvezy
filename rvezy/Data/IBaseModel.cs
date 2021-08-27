using System;

namespace rvezy.Data
{
    public interface IId
    {
        Guid Id { get; set; }
    }

    public interface IBaseModel : IId
    {
        DateTimeOffset? CreatedOn { get; set; }
        
        Guid? CreatedBy { get; set; }
        
        DateTimeOffset? ModifiedOn { get; set; }
        
        Guid? ModifiedBy { get; set; }
        
        bool Deleted { get; set; }

        DateTimeOffset? DeletedOn { get; set; }

        Guid? DeletedBy { get; set; }
    }
}