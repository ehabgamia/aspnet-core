using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurant
{
    [Table("RequestLogs")]
    public class RequestLog : Entity
    {
        [ForeignKey("RestRequestId")]
        public RestRequest RestRequest { get; set; }
        public int RestRequestId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("RequestStatusId")]
        public RequestStatus RequestStatus { get; set; }
        public int RequestStatusId { get; set; }

        public DateTime ActionDateTime { get; set; }

    }
}
