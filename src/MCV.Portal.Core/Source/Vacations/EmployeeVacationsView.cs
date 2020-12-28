using Abp.Domain.Entities;
using MCV.Portal.Source.VacationTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.Vacations
{
    [Table("VacationsEmployeeView")]
    public class EmployeeVacationsView : Entity, ISoftDelete
    {

        [ForeignKey("VacationTypeId")]
        public virtual VacationType VacationType { get; set; }

        public virtual int VacationTypeId { get; set; }

        public VacationStatus Status { get; set; }
      
        public int EmpSAPCode { get; set; }

        public string Requester { get; set; }

        public DateTime VacationFrom { get; set; }

        public DateTime VacationTo { get; set; }

        public DateTime CreationTime { get; set; }

        public string OnBehalfEmp { get; set; }

        public string Reason { get; set; }
        
        public string Manager { get; set; }

        public virtual bool IsDeleted { get; set; }
    }
}
