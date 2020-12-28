using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MCV.Portal.Source.AnnouncementsTypes.Dto
{
    public class CreateAnnouncementTypeInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string IconPath { get; set; }

    }
}
