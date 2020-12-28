using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurant
{
    [Table("RestRequestItems")]
    public class RestRequestItem : FullAuditedEntity
    {
        [ForeignKey("RestScheduleItemsId")]
        public RestScheduleItem RestScheduleItems { get; set; }
        public virtual int? RestScheduleItemsId { get; set; }

        [ForeignKey("RestRequestsId")]
        public RestRequest RestRequests { get; set; }
        public virtual int RestRequestsId { get; set; }

        [ForeignKey("RestNonSchItemId")]
        public RestNonSchItem RestNonSchItem { get; set; }
        public virtual int? RestNonSchItemId { get; set; }

        public virtual int Count { get; set; }

    }
}
