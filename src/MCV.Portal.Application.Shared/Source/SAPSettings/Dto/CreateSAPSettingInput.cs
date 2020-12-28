using System;
using System.Collections.Generic;
using System.Text;

namespace MCV.Portal.Source.SAPSettings.Dto
{
    public class CreateSAPSettingInput
    {
        public string ConnectionName { get; set; }
        public string ServerHost { get; set; }
        public int SystemNumber { get; set; }
        public string SystemID { get; set; }
        public int UserName { get; set; }
        public string Password { get; set; }
        public int Client { get; set; }
        public string Language { get; set; }
        public int PoolSize { get; set; }
        public bool DefaultConnection { get; set; }
    }
}
