using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurant
{
    [Table("RestSchedules")]
    public class RestSchedule : FullAuditedEntity
    {
        public virtual DateTime CurrentDate { get; set; }

        public virtual string Day { get; set; }

        [ForeignKey("RestInfoID")]
        public int RestInfoID { get; set; }
        public virtual RestInfo RestInfos { get; set; }

        public virtual ICollection<RestScheduleItem> RestScheduleItems { get; set; }

    }
}
