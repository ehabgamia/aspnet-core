using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class GetRestScheForEditOutput
    {
        public int Id { get; set; }

        public virtual DateTime CurrentDate { get; set; }

        public virtual Days Days { get; set; }

        public string Day { get; set; }

        [ForeignKey("RestInfoID")]
        public int RestInfoID { get; set; }

        public List<RestItemsListDto> targetItems { get; set; }

    }
}
