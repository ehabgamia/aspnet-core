using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurant
{
    [Table("RestResponses")]
    public class RestResponse : FullAuditedEntity
    {
        [ForeignKey("RestRequestItemsId")]
        public RestRequestItem RestRequestItems { get; set; }
        public virtual int RestRequestItemsId { get; set; }

        public virtual bool NotAvailable { get; set; }

        public virtual bool InternalCancel { get; set; }
    }
}
