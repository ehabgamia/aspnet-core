
namespace MCV.Portal.Source.Restaurants.Dto
{
    public class RestInfoAdminsDto
    {
        public int emp_id { get; set; }

        public int RestInfoID { get; set; }
        public virtual RestInfosListDto RestInfos { get; set; }
    }
}
