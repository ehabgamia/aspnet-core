using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class CreateNonSchRestInput
    {
        [ForeignKey("RestInfoID")]
        public int RestInfoID { get; set; }

        public List<RestItemsListDto> targetItems { get; set; }
    }
}
