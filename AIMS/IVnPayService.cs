using AIMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS
{
    internal interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformation model);
        PaymentResponseModel PaymentExecute(Dictionary<string, string> responseData);
    }
}
