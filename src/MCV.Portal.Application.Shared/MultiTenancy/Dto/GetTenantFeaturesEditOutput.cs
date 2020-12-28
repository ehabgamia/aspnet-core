using System.Collections.Generic;
using Abp.Application.Services.Dto;
using MCV.Portal.Editions.Dto;

namespace MCV.Portal.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}