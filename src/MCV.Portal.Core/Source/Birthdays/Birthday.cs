using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.Birthdays
{
    [Table("GetBirthdaysToday")]
    public class Birthday : Entity
    {
        [Column("BirthName")]
        public virtual string BirthName { get; set; }

        [Column("ImgPath")]
        public virtual string ImgPath { get; set; }
    }
}
