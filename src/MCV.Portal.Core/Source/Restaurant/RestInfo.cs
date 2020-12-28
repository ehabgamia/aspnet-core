using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurant
{
    [Table("RestInfos")]
    public class RestInfo : FullAuditedEntity
    {
        public virtual string Country { get; set; }

        public virtual string City { get; set; }

        public virtual string Area { get; set; }

        public virtual string Building { get; set; }

        public virtual bool isAvailable { get; set; }

        public virtual string ExtNum { get; set; }

        [ForeignKey("RestCategoryId")]
        public virtual RestCategory RestCategory { get; set; }

        public int? RestCategoryId { get; set; }

        public virtual ICollection<RestSchedule> RestSchedules { get; set; }

        public virtual ICollection<RestInfoAdmins> RestInfoAdmins { get; set; }
    }
}
