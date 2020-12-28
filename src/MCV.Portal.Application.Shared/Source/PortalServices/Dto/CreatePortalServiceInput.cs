using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MCV.Portal.Source.PortalServices.Dto
{
    public class CreatePortalServiceInput
    {
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public string ServiceDescription { get; set; }
    }
}
