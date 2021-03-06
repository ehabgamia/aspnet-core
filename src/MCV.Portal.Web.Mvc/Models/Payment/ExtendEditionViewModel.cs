﻿using System.Collections.Generic;
using MCV.Portal.Editions.Dto;
using MCV.Portal.MultiTenancy.Payments;

namespace MCV.Portal.Web.Models.Payment
{
    public class ExtendEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}