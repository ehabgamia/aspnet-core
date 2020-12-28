using Abp.Domain.Entities.Auditing;
using MCV.Portal.Authorization.Users;
using MCV.Portal.Source.Vacations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.VacationTypes
{
    [Table("VacationTypes")]
    public class VacationType : FullAuditedEntity
    {
        public virtual string TypeOfVacation { get; set; }

        public virtual int SAPCodeType { get; set; }

        public virtual int  ServiceDeskId { get; set; }

        [ForeignKey("CreatorUserId")]
        public virtual User User { get; set; }

        public virtual bool RequireDateTime { get; set; }

        public virtual bool RequireTo { get; set; }

        public virtual VacationUnit Unit { get; set; }

        public virtual decimal Limit { get; set; }

    }
}
