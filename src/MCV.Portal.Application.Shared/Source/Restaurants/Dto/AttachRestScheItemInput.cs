using System.Collections.Generic;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class AttachRestScheItemInput
    {
        public int Id { get; set; }

        public int RestSchedulesId { get; set; }

        public int RestItemsId { get; set; }

       public List<RestItemsListDto> selecteditem { get; set; }

        public List<RestItemsListDto> unselecteditem { get; set; }


    }
}
