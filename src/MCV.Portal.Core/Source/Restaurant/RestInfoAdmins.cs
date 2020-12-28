using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.Restaurant
{
    [Table("RestInfoAdmins")]
    public class RestInfoAdmins : FullAuditedEntity
    {
        public int emp_id { get; set; }

        [ForeignKey("RestInfoID")]
        public int RestInfoID { get; set; }
        public virtual RestInfo RestInfos { get; set; }
    }
}
