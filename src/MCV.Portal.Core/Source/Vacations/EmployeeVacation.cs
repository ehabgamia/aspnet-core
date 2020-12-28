using Abp.Domain.Entities.Auditing;
using MCV.Portal.Source.VacationTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.Vacations
{
    [Table("VacationHRSystem")]
    public class EmployeeVacation : FullAuditedEntity
    {
        public string Requester { get; set; }

        [ForeignKey("VacationTypeId")]
        public virtual VacationType VacationType { get; set; }
        public virtual int VacationTypeId { get; set; }

        public int EmpSAPCode { get; set; }

        public DateTime VacationFrom { get; set; }

        public DateTime VacationTo { get; set; }

        public decimal Period { get; set; }

        public int OnBehalfSAPCode { get; set; }

        public virtual VacationStatus Status { get; set; }

        public string Reason { get; set; }

    }
}
