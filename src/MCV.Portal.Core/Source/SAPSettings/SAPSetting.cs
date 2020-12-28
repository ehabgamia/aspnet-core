using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.SAPSettings
{

    [Table("SAPSettings")]
    public class SAPSetting : FullAuditedEntity
    {
        public virtual string ConnectionName { get; set; }
        public virtual string ServerHost { get; set; }
        public virtual int SystemNumber { get; set; }
        public virtual string SystemID { get; set; }
        public virtual int UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual int Client { get; set; }
        public virtual string Language { get; set; }
        public virtual int PoolSize { get; set; }
        public virtual bool DefaultConnection { get; set; }


    }
}
