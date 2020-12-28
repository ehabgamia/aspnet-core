using System;
using System.Collections.Generic;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class RestSchesListDto
    {
        public int Id { get; set; }

        public DateTime CurrentDate { get; set; }

        public int Total { get; set; }

        public Days Days { get; set; }

        public string Day { get; set; }

        public int RestInfoID { get; set; }

        public RestInfosListDto RestInfosListDtos { get; set; }

        public string RestaurantInfo { get; set; }

        public RestItemsListDto restItemsListDto { get; set; }

        public int RestItemId { get; set; }

        public string Date { get; set; }
    }
}
