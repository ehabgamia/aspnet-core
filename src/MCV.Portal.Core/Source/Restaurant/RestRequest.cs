using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurant
{
    [Table("RestRequests")]
    public class RestRequest : FullAuditedEntity
    {
        [Required]
        public virtual long UserId { get; set; }

        public virtual long RequesterId { get; set; }

        [ForeignKey("RestSchedulesId")]
        public RestSchedule RestSchedules { get; set; }
        public virtual int? RestSchedulesId { get; set; }

        [ForeignKey("RestInfosID")]
        public RestInfo RestInfos { get; set; }
        public virtual int? RestInfosID { get; set; }

        [ForeignKey("RequestStatusID")]
        public RequestStatus RequestStatus { get; set; }
        public virtual int? RequestStatusID { get; set; }

        [ForeignKey("PaymentTypeID")]
        public PaymentType PaymentTypes { get; set; }
        public virtual int? PaymentTypeID { get; set; }

    }
}
