using AIMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace AIMS.Services
{
    public class VnPayService
    {
        private const string VnPayUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public string GetClientIpAddress()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);

            foreach (IPAddress ipAddress in ipAddresses)
            {
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ipAddress.ToString();
                }
            }

            return "Unknown";
        }
        public string CreatePaymentUrl(OrderData orderData)
        {
            string VnPayTmnCode = "OKB63LF1";
            string VnPayHashSecret = "21QHG4J48NZYC1I54IXEBB0TP5CB1BSB";
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            //var urlCallBack = "https://localhost:5001/Home/PaymentCallback";
            var urlCallBack = "https://thanhlc.com";

            var expireTime = timeNow.AddHours(1);
            pay.AddRequestData("vnp_Version", "2.1.0");
            pay.AddRequestData("vnp_Command", "pay");
            pay.AddRequestData("vnp_TmnCode", VnPayTmnCode);
            pay.AddRequestData("vnp_Amount", ((int)orderData.totalPrice * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", "VND");
            pay.AddRequestData("vnp_IpAddr", GetClientIpAddress());
            pay.AddRequestData("vnp_Locale", "vn");
            pay.AddRequestData("vnp_OrderInfo", $"Order{orderData.phone}");
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_ExpireDate", expireTime.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_TxnRef", tick);
            //pay.AddRequestData("vnp_BankCode", "NCB");
            pay.AddRequestData("vnp_OrderType", "other"); 
            var paymentUrl =
                pay.CreateRequestUrl(VnPayUrl, VnPayHashSecret);
            return paymentUrl;
        }

        public PaymentResponseModel PaymentExecute(Dictionary<string, string> responseData)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(responseData, "21QHG4J48NZYC1I54IXEBB0TP5CB1BSB");

            return response;
        }
    }
}