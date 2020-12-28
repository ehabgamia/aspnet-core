using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurant
{
    [Table("PaymentTypes")]
    public class PaymentType : Entity
    {
        public virtual string PaymentTypeDesc { get; set; }
    }
}
