using AIMS.Controllers.Payment;
using AIMS.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace AIMS.Views.Payment
{
    public partial class PaymentView : UserControl
    {
        private readonly PaymentController _paymentController;
        private OrderData orderData;
        public PaymentView(OrderData orderData)
        {
            InitializeComponent();
            _paymentController = new PaymentController();
            this.orderData = orderData;
        }

        private async void PaymentView_Load(object sender, EventArgs e)
        {
            string paymentUrl = _paymentController.createPaymentUrl(orderData);
            await webView21.EnsureCoreWebView2Async();
            System.IO.File.WriteAllText("url.txt", paymentUrl);
            webView21.CoreWebView2.Navigate(paymentUrl);
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.AbsoluteUri.StartsWith("http://your-return-url.com"))
            {
                // Lấy chuỗi truy vấn từ URL
                var queryParams = e.Url.Query.TrimStart('?')
                    .Split('&')
                    .Select(q => q.Split('='))
                    .ToDictionary(kv => kv[0], kv => Uri.UnescapeDataString(kv[1]));

                // Kiểm tra kết quả thanh toán
            }
        }
        private const string PaymentApiUrl = "https://thanhlc.com/Payment/ProcessPayment";
        private async void webView21_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                string currentUrl = webView21.Source.ToString();
                if (currentUrl.Contains("thanhlc.com"))
                {
                    // Add Order vào CSDL 
                    // Chuyển tới trang kết quả thanh toán 
                }
                if (currentUrl.Contains("vnp_Amount") && currentUrl.Contains("vnp_TxnRef"))
                {
                    // Lấy các tham số từ URL
                    var uri = new Uri(currentUrl);
                    var queryParams = HttpUtility.ParseQueryString(uri.Query);

                    // Chuyển đổi queryParams thành Dictionary<string, string>
                    var responseData = new Dictionary<string, string>();
                    foreach (string key in queryParams.AllKeys)
                    {
                        responseData.Add(key, queryParams[key]);
                    }

                    // Gửi request đến server để xử lý thông tin thanh toán
                    using (var httpClient = new HttpClient())
                    {
                        try
                        {
                            // Tạo request data
                            var requestData = new FormUrlEncodedContent(responseData);

                            // Gửi POST request đến server
                            var response = await httpClient.PostAsync(PaymentApiUrl, requestData);

                            // Đọc response từ server
                            if (response.IsSuccessStatusCode)
                            {
                                string responseContent = await response.Content.ReadAsStringAsync();

                                // In response ra file JSON
                                string jsonFilePath = "PaymentResponse.json";
                                File.WriteAllText(jsonFilePath, responseContent);

                                // Deserialize JSON thành PaymentResponseModel (nếu cần)
                                var paymentResponse = JsonConvert.DeserializeObject<PaymentResponseModel>(responseContent);

                                if (paymentResponse.Success)
                                {
                                    MessageBox.Show($"Thanh toán thành công! Mã giao dịch: {paymentResponse.TransactionId}, Số tiền: {paymentResponse.TransactionId}. Kiểm tra file JSON trên Desktop.");
                                    // Cập nhật UI
                                }
                                else
                                {
                                    MessageBox.Show($"Thanh toán thất bại! Mã lỗi: {paymentResponse.VnPayResponseCode}. Kiểm tra file JSON trên Desktop.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Lỗi khi gửi yêu cầu đến server.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void webView21_Click(object sender, EventArgs e)
        {

        }
    }
}
