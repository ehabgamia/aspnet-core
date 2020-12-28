using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class GetRestRespForEditOutput
    {
        public int Id { get; set; }

        [ForeignKey("RestRequestsId")]
        public RestReqListDto RestRequests { get; set; }
        public virtual int RestRequestsId { get; set; }
    }
}
