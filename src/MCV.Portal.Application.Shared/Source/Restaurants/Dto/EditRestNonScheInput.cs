using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class EditRestNonScheInput
    {
        public int Id { get; set; }

        [ForeignKey("RestInfoID")]
        public int RestInfoID { get; set; }

        public List<RestItemsListDto> targetItems { get; set; }
    }
}
