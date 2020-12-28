namespace MCV.Portal.Source.Restaurants.Dto
{
    public class RestNonSchesListDto
    {
        public int Id { get; set; }

        public int RestItemId { get; set; }

        public int RestInfoID { get; set; }

        public RestItemsListDto restItemsListDto { get; set; }

        public double SpecialSalesPrice { get; set; }
    }
}
