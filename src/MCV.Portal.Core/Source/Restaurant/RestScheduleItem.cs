using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurant
{
    [Table("RestScheduleItems")]
    public class RestScheduleItem : FullAuditedEntity
    {
        [ForeignKey("RestItemsId")]
        public RestItem RestItems { get; set; }
        public virtual int RestItemsId { get; set; }

        [ForeignKey("RestSchedulesId")]
        public RestSchedule RestSchedules { get; set; }
        public virtual int RestSchedulesId { get; set; }

    }
}
