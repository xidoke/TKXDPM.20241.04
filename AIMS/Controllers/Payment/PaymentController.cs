using AIMS.Models.Entities;
using AIMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;

namespace AIMS.Controllers.Payment
{
    public class PaymentController : ControllerBase
    {
        private readonly VnPayService _vnPayService;

        public PaymentController()
        {
            _vnPayService = new VnPayService();
        }
        public string createPaymentUrl(OrderData orderData)
        {
            return _vnPayService.CreatePaymentUrl(orderData);
        }
        [HttpPost]
        public IActionResult ProcessPayment([FromForm] Dictionary<string, string> responseData)
        {
            var paymentResponse = _vnPayService.PaymentExecute(responseData);
            return Ok(paymentResponse);
        }
    }

}
