using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class CreateRestScheInput
    {
        public int Id { get; set; }

        public DateTime CurrentDate { get; set; }

        public Days Day { get; set; }

        [ForeignKey("RestInfoID")]
        public int RestInfoID { get; set; }

        public List<RestItemsListDto> targetItems { get; set; }

        public bool Schedule { get; set; }

        public bool NonSchedule { get; set; }

    }
}
