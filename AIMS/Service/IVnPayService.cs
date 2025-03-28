﻿using AIMS.Models;
using AIMS.ViewModels;

namespace AIMS.Service
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPayRequest model);
        VnPayResponse PaymentExecute(IQueryCollection collections);
    }
}
