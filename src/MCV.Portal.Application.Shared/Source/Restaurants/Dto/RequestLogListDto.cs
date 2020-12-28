using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class RequestLogListDto
    {
        [ForeignKey("RestRequestId")]
        public RestReqListDto RestRequest { get; set; }
        public int RestRequestId { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }


        [ForeignKey("RequestStatusId")]
        public ReqStatusListDto RequestStatus { get; set; }
        public int RequestStatusId { get; set; }

        public virtual string StatusDesc { get; set; }

        public DateTime ActionDateTime { get; set; }

        public string DateTime { get; set; }
    }
}
