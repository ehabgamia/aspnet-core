using System;
using System.Collections.Generic;
using System.Text;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class GetRestScheItemForAttachOutput
    {
        public int Id { get; set; }

        public int RestSchedulesId { get; set; }

        public int RestItemsId { get; set; }
    }
}
