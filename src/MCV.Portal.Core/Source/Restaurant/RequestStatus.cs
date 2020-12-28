using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurant
{
    [Table("RequestStatus")]
    public class RequestStatus : Entity
    {
        public virtual string StatusDesc { get; set; }
    }
}
