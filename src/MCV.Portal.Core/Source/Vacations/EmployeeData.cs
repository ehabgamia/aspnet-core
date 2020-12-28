using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.Vacations
{

    [Table("VacationsGetEmpData")]
    public class EmployeeData : Entity
    {

        [Column("BirthName")]
        public string BirthName { get; set; }

        [Column("Position")]
        public string Position { get; set; }

        [Column("EmployeeType")]
        public int EmployeeType { get; set; }

        [Column("OrgName")]
        public string OrgName { get; set; }

        [Column("Manager")]
        public string Manager { get; set; }
    }
}
