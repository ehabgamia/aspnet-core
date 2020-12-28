using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
   public class EditRestReqInput
    {
        public int Id { get; set; }

        public long UserId { get; set; }

        public long RequesterId { get; set; }

        [ForeignKey("RestSchedulesId")]
        public RestSchesListDto RestSchedules { get; set; }
        public int RestSchedulesId { get; set; }

        public int RestInfosID { get; set; }

        public List<RestItemsListDto> targetItems { get; set; }

        public bool PersonalCost { get; set; }

    }
}
