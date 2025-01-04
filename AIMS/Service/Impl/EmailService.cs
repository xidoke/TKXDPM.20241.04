using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AIMS.Data.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AIMS.ViewModels;


namespace AIMS.Service.Impl
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendOrderDetailsAsync(OrderDetailsEmailViewModel model)
        {
            var subject = $"AIMS - Chi tiết đơn hàng #{model.Order.Id}";
            var body = GenerateOrderDetailsEmailBody(model);
            await SendEmailAsync(model.Order.Email, subject, body);
        }

        private string GenerateOrderDetailsEmailBody(OrderDetailsEmailViewModel model)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine($"<title>Chi tiết đơn hàng #{model.Order.Id}</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("table { width: 100%; border-collapse: collapse; }");
            sb.AppendLine("th, td { border: 1px solid black; padding: 8px; text-align: left; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine($"<h2>AIMS - Chi tiết đơn hàng #{model.Order.Id}</h2>");
            sb.AppendLine($"<p>Ngày: {model.Order.CreatedAt}</p>");
            sb.AppendLine("<h3>Thông tin đơn hàng</h3>");
            sb.AppendLine($"<p><b>Mã đơn hàng:</b> {model.Order.Id}</p>");
            sb.AppendLine($"<p><b>Mã giao dịch:</b> {model.TransactionId}</p>");
            sb.AppendLine($"<p><b>Nội dung giao dịch:</b> {model.TransactionContent}</p>");
            sb.AppendLine($"<p><b>Ngày giao dịch:</b> {model.TransactionDate}</p>");
            sb.AppendLine("<h3>Danh sách sản phẩm</h3>");
            sb.AppendLine("<table>");
            sb.AppendLine("<thead><tr><th>Tên sản phẩm</th><th>Số lượng</th><th>Đơn giá</th><th>Thành tiền</th></tr></thead>");
            sb.AppendLine("<tbody>");
            foreach (var item in model.OrderMedias)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{item.Name}</td>");
                sb.AppendLine($"<td>{item.Quantity}</td>");
                sb.AppendLine($"<td>{item.Price} VND</td>");
                sb.AppendLine($"<td>{item.Quantity * item.Price} VND</td>");
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");
            sb.AppendLine($"<p><b>Tổng tiền (Chưa bao gồm phí VAT):</b> {model.TotalProductPriceExcludingVAT} VND</p>");
            sb.AppendLine($"<p><b>Tổng tiền (Đã bao gồm phí VAT):</b> {model.TotalProductPriceIncludingVAT} VND</p>");
            sb.AppendLine($"<p><b>Phí vận chuyển:</b> {model.Order.ShippingFee} VND</p>");
            sb.AppendLine($"<p><b>Số tiền đã thanh toán:</b> {model.Order.TotalPrice} VND</p>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            return sb.ToString();
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpServer = _configuration["SmtpSettings:Server"];
                var smtpPort = int.Parse(_configuration["SmtpSettings:Port"]);
                var smtpUsername = _configuration["SmtpSettings:Username"];
                var smtpPassword = _configuration["SmtpSettings:Password"];
                var enableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"]);
                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.EnableSsl = enableSsl;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    var message = new MailMessage
                    {
                        From = new MailAddress(smtpUsername),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8
                    };
                    message.To.Add(toEmail);
                    await client.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw;
            }
        }
    }
}
