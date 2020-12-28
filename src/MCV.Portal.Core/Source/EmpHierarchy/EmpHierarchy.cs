using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.EmpHierarchy
{
    [Table("SAPEmpHierarchy")]
    public class EmpHierarchy : Entity
    {
        public int SAPCode { get; set; }

        public int OrgCode { get; set; }
        
        public string SLevel1 { get; set; }
        
        public int SLevel1SAPCode { get; set; }
        
        public string SLevel2 { get; set; }
        
        public int? SLevel2SAPCode { get; set; }
        
        public string SLevel3 { get; set; }
        
        public int? SLevel3SAPCode { get; set; }

        public int ServiceId { get; set; }
    }
}
